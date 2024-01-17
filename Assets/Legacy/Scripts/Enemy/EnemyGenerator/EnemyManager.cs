using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;

namespace SoulRun.InGame
{
    /// <summary>
    /// シーン上にあるスポーンポイントの情報を取得し、
    /// Playerとスポーンポイントの位置が一定以下になったらスポーンポイントに登録されている生成すべき敵をFactoryから取得する
    /// 取得した敵は監視対象としてリストに保持しておきPlayerとの距離を計測する。このリストは追加されるたびに位置がプレイヤーに近い順にソートする
    /// 毎フレーム監視対象リストの位置を確認する。もしPlayerとの位置が離れていたらその時点でそのフレームでの位置の走査は終わりにする
    /// 対象がカメラの位置よりも後ろに来たらPoolに返却する
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private float _generateDistanceFromPlayer = 10;
        /// <summary> シーン上にあるスポナー(Z座標が0から遠い順にソート) </summary>
        private List<SpawnPoint> _enemySpawnerList;
        [Inject] private PlayerManager _playerManager;
        [Inject] private PlayerScoreManager _playerScoreManager;
        [Inject] private PlayerExpManager _playerExpManager;
        [Inject] private InGameManager _inGameManager;
        /// <summary> 現在アクティブなエネミーのリスト(Z座標が0に近い順にソート) </summary>
        private List<(Enemy enemy, AbstractFactory<Enemy> factory)> _activeEnemyInfoList = new();
        /// <summary> 生成可能距離 </summary>
        private List<AbstractFactory<Enemy>> _factoryList = new List<AbstractFactory<Enemy>>();
        private List<(Enemy enemy, AbstractFactory<Enemy> factory)> enemiesToRemove = new List<(Enemy, AbstractFactory<Enemy>)>();  //廃棄する敵のリスト
        [SerializeField] List<GameObject> bossEnemies;

        private void Start()
        {
            _enemySpawnerList = FindObjectsByType<SpawnPoint>(sortMode: FindObjectsSortMode.None).ToList();
            //エディタ拡張であらかじめソートしてもいいかも。遠い順にしたのは要素を削除するのに楽だから
            _enemySpawnerList.Sort((a, b) => b.transform.position.z.CompareTo(a.transform.position.z));
            //ここで生成する敵のオブジェクト分Factoryを用意
            PrepareFactory();

            _inGameManager.CurrentState.Where(state => state == InGameManager.GameState.Playing).Subscribe(_ => PoseEnemy(false));
            _inGameManager.CurrentState.Where(state => state == InGameManager.GameState.Gacha).Subscribe(_ => PoseEnemy(true));
            _inGameManager.CurrentState.Where(state => state == InGameManager.GameState.BossStage).Subscribe(_ => SetBoss());
        }

        private void PrepareFactory()
        {
            foreach (var spawnPoint in _enemySpawnerList)
            {
                bool _isFactoryListContainThisSpawnPointSpawnObject = false;
                //ここですでに同じPrefabを参照するAbstractFactory<Enemy>を登録しているかを判定する
                foreach (var factory in _factoryList)
                {   //あるなら作らない
                    if (factory.InstantiateObj == spawnPoint.GenerateEnemy)
                    {
                        _isFactoryListContainThisSpawnPointSpawnObject = true;
                        break;
                    }
                }
                if (_isFactoryListContainThisSpawnPointSpawnObject) continue;

                var objPool = this.gameObject.AddComponent<GameObjectPoolForUnity>();
                var enemyFactory = new EnemyFactory(objPool, spawnPoint.GenerateEnemy, _playerScoreManager, _playerExpManager);
                _factoryList.Add(enemyFactory);
            }
        }

        private void Update()
        {
            FindAvailableSpawner();
            FindUnavailableEnemy();
        }

        /// <summary>
        /// 起動可能なスポナーを探し起動する
        /// </summary>
        private void FindAvailableSpawner()
        {
            //まずプレイヤーの位置と各スポナーの位置を見て生成距離に入ったらプールから取ってくる
            for (int i = _enemySpawnerList.Count - 1; i >= 0; i--)
            {
                if (_enemySpawnerList[i].transform.position.z > _playerManager.transform.position.z + _generateDistanceFromPlayer)
                    return; // 範囲外なら以降も範囲外なのでリターン
                ActiveSpawner(_enemySpawnerList[i]);
                _enemySpawnerList.RemoveAt(i);
            }
        }

        /// <summary>
        /// スポナーを起動する
        /// </summary>
        /// <param name="spawnPoint"></param>
        private void ActiveSpawner(SpawnPoint spawnPoint)
        {
            foreach (var factory in _factoryList)
            {   //目的のFactoryを探す
                if (factory.InstantiateObj == spawnPoint.GenerateEnemy)
                {   //敵を生成し、キャッシュ
                    var enemyObj = factory.Create();
                    enemyObj.transform.position = spawnPoint.transform.position;
                    enemyObj.SetActive(true);
                    var enemy = enemyObj.GetComponent<Enemy>();
                    enemy.OnReturnMethod += () => factory.ReturnObject(enemyObj);
                    enemy.Active();
                    _activeEnemyInfoList.Add((enemy, factory));
                    return;
                }
            }
        }

        /// <summary>
        /// 現在アクティブなエネミーでカメラの後ろにいったものをプールに返却する
        /// </summary>
        private void FindUnavailableEnemy()
        {
            foreach (var activeEnemyInfo in _activeEnemyInfoList)
            {
                if (activeEnemyInfo.enemy.transform.position.z < Camera.main.transform.position.z)
                {
                    enemiesToRemove.Add(activeEnemyInfo);
                }
            }

            // イテレート後に敵を削除
            foreach (var enemyToRemove in enemiesToRemove)
            {
                enemyToRemove.factory.ReturnObject(enemyToRemove.enemy.gameObject);
                _activeEnemyInfoList.Remove(enemyToRemove);
            }

            enemiesToRemove.Clear();
        }

        /// <summary>
        /// 管理しているアクティブな敵をポーズさせたり、動かしたりするクラス
        /// </summary>
        /// <param name="isPose">trueでポーズさせる</param>
        private void PoseEnemy(bool isPose)
        {
            if (isPose)
            {   //ポーズさせる
                foreach (var enemyInfo in _activeEnemyInfoList)
                {
                    enemyInfo.enemy.TryGetComponent<IPausable>(out var posable);
                    posable.Pause();
                }
            }
            else
            {
                foreach (var enemyInfo in _activeEnemyInfoList)
                {
                    enemyInfo.enemy.TryGetComponent<IPausable>(out var posable);
                    posable.Active();
                }
            }
        }

        private void SetBoss()
        {
            var boss = Instantiate(bossEnemies[0]);
            var bossComp = boss.GetComponent<Enemy>();
            bossComp.Active();
            bossComp.OnDeathMethod += () =>
            {
                _inGameManager.BossEnd().Forget();
                Destroy(boss);
            };
            bossEnemies.RemoveAt(0);

        }
    }
}
