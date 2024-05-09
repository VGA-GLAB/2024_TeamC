using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.Common.Interface;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Entityの生成管理クラス
    /// </summary>
    public class EntitySpawnerController : MonoBehaviour
    {
        [SerializeField, CustomLabel("生成するEntity")]
        List<DamageableEntity> _fieldEntity;

        [SerializeReference, SubclassSelector, CustomLabel("生成条件")]
        ISpawnerEnableType _spawnerType;
        
        [SerializeReference, SubclassSelector, CustomLabel("敵生成パターン")]
        ISpawnPattern _spawnPattern;

        [SerializeField, CustomLabel("生成インターバル (ミリ秒)"), Range(0, 1000)]
        float _spawnInterval;

        [SerializeField, CustomLabel("イラスト左右反転化")]
        bool _useRandomFlip;

        [SerializeField, CustomLabel("スポナーが使用可能か")] bool _isSpawnerAvailable = true;

        
        //現状はヒットしたplayerの参照をヒット時に格納する
        PlayerManager _playerManager;
        bool _spawnFlag;
        public ISpawnerEnableType SpawnerType => _spawnerType;

        void Start()
        {
            GameObject.FindWithTag("Player").TryGetComponent(out _playerManager);
            if (_spawnerType == null)
            {
                Debug.LogWarning($"{gameObject.name} の生成条件がnullです。生成処理をロックします。");
                _isSpawnerAvailable = false;
            }
        }

        void Update()
        {
            if (_spawnFlag　|| !_isSpawnerAvailable) return;
            if (_spawnerType.IsEnable(_playerManager.transform.position, transform.position))
            {
                StartCoroutine(SpawnEntity());
                _spawnFlag = true;
            }
        }

        // TODO たぶんオブジェクトプールが必要になる気がする

        /// <summary>
        /// GetSpawnPositionsで渡された場所にEntityを召喚するメソッド
        /// </summary>
        public IEnumerator SpawnEntity()
        {
            if (_spawnPattern == null)
            {
                Debug.LogWarning($"{gameObject.name} の生成パターンが設定されていません");
                yield break;
            }

            var spawnIndex = 0;
            foreach (var pos in _spawnPattern.GetSpawnPositions())
            {
                if (_fieldEntity.Count <= spawnIndex)
                {
                    spawnIndex = 0;
                }

                yield return new WaitForSeconds(_spawnInterval / 1000);

                var entity = ObjectPoolManager.Instance.RequestInstance(_fieldEntity[spawnIndex], transform.position + pos.Item1,
                    Quaternion.Euler(0, pos.Item2, 0));
                //entity.SetPlayer(_playerManager);
                spawnIndex++;

                if (entity.TryGetComponent(out SpriteRenderer renderer) && _useRandomFlip)
                {
                    // 1/2で左右反転
                    renderer.flipX = Random.Range(0, 2) == 0;
                }
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            var position = transform.position;
            _spawnerType?.DrawSpawnerArea(position);
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