using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Entityの生成管理クラス
    /// </summary>
    [RequireComponent(typeof(SphereCollider))]
    public class EntitySpawnController : MonoBehaviour
    {
        [SerializeField, Tooltip("生成するEntity")]
        List<FieldEntityController> _fieldEntity;

        [SerializeField, Tooltip("生成開始距離")] float _spawnerEnableRange;

        [SerializeReference, SubclassSelector, Tooltip("生成パターン")]
        ISpawnPattern _spawnPattern;
        //現状はヒットしたplayerの参照をヒット時に格納する
        PlayerManager _playerManager;

        void Start()
        {
            var spawnRangeCollider = GetComponent<SphereCollider>();
            if (!spawnRangeCollider.isTrigger)
            {
                Debug.LogError($"{gameObject.name} の SphereCollider が IsTrigger に設定されていません");
            }

            spawnRangeCollider.radius = _spawnerEnableRange;
        }


        /// <summary>
        /// GetSpawnPositionsで渡された場所にEntityを召喚するメソッド
        /// </summary>
        public void SpawnEntity()
        {
            if (_spawnPattern == null)
            {
                Debug.LogWarning($"{gameObject.name} の生成パターンが設定されていません");
                return;
            }

            var spawnIndex = 0;
            foreach (var pos in _spawnPattern.GetSpawnPositions())
            {
                if (_fieldEntity.Count <= spawnIndex)
                {
                    spawnIndex = 0;
                }
                
                // TODO 複数種出す場合、それらを選択するロジックを考える
                var entity = Instantiate(_fieldEntity[spawnIndex], transform.position + pos, Quaternion.identity);
                entity.SetPlayer(_playerManager);
                spawnIndex++;
            }
        }

        /// <summary>
        /// スポナー感知処理（仮でコライダー式）
        /// </summary>
        void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            _playerManager = other.gameObject.GetComponent<PlayerManager>();
            SpawnEntity();
        }

        // bool CheckPlayerDistance(Transform playerTrans)
        // {
        //     return true;
        // }
    }
}