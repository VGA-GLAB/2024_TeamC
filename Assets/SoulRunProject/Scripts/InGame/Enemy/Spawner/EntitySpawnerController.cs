using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using SoulRunProject.Common.Interface;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulRunProject.InGame
{
    /// <summary>
    ///     Entityの生成管理クラス
    /// </summary>
    public class EntitySpawnerController : MonoBehaviour
    {
        [SerializeField] [CustomLabel("生成するEntity")]
        private List<DamageableEntity> _fieldEntity;

        [SerializeReference] [SubclassSelector] [CustomLabel("敵生成パターン")]
        private ISpawnPattern _spawnPattern;

        [SerializeField] [CustomLabel("生成インターバル (秒)")] [Range(0, 60)]
        private float _spawnInterval;

        [SerializeField] [CustomLabel("イラスト左右反転化")]
        private bool _useRandomFlip;

        [SerializeField] [CustomLabel("スポナーが使用可能か")]
        private bool _isSpawnerAvailable = true;

        //現状はヒットしたplayerの参照をヒット時に格納する
        private PlayerManager _playerManager;
        private bool _spawnFlag;

        [SerializeField] [SerializeReference] [SubclassSelector] [CustomLabel("生成条件")]
        private ISpawnerEnableType _spawnerType;


        public ISpawnerEnableType SpawnerType => _spawnerType;

        private void Start()
        {
            GameObject.FindWithTag("Player").TryGetComponent(out _playerManager);
            if (SpawnerType == null)
            {
                Debug.LogWarning($"{gameObject.name} の生成条件がnullです。生成処理をロックします。");
                _isSpawnerAvailable = false;
            }
        }

        private void Update()
        {
            if (_spawnFlag　|| !_isSpawnerAvailable) return;
            if (SpawnerType.IsEnable(_playerManager.transform.position, transform.position))
            {
                SpawnEntity().Forget();
                _spawnFlag = true;
            }
        }

        /// <summary>
        ///     GetSpawnPositionsで渡された場所にEntityを召喚するメソッド
        /// </summary>
        private async UniTaskVoid SpawnEntity()
        {
            if (_spawnPattern == null)
            {
                Debug.LogWarning($"{gameObject.name} の生成パターンが設定されていません");
                return;
            }

            var spawnIndex = 0;
            foreach (var pos in _spawnPattern.GetSpawnPositions())
            {
                if (_fieldEntity.Count <= spawnIndex) spawnIndex = 0;

                var result = await PauseManager.TryWaitForSeconds(_spawnInterval, destroyCancellationToken);
                if (!result) return;

                var entity = ObjectPoolManager.Instance.RequestInstance(_fieldEntity[spawnIndex],
                    transform.position + pos.Item1,
                    Quaternion.Euler(0, pos.Item2, 0));
                spawnIndex++;

                if (entity.TryGetComponent(out SpriteRenderer renderer) && _useRandomFlip)
                    // 1/2で左右反転
                    renderer.flipX = Random.Range(0, 2) == 0;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var position = transform.position;
            SpawnerType?.DrawSpawnerArea(position);
            _spawnPattern?.DrawGizmos(position);

            // TODO シーン上で生成パターンを見れるようにしたいね
            // _spawnFlag = false;
            // foreach (var pos in _spawnPattern.GetSpawnPositions())
            // {
            //     Gizmos.color = Color.red;
            //     Gizmos.DrawWireSphere(pos, 1);
            // }
        }
        //
        // public void SpawnEditorEntity()
        // {
        //     _spawnFlag = true;
        // }
#endif
    }
}