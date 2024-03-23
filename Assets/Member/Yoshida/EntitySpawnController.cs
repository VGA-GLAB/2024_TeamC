using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Entityの生成管理クラス
    /// </summary>
    public class EntitySpawnController : MonoBehaviour
    {
        [SerializeField, Tooltip("生成するEntity")]
        List<FieldEntityController> _fieldEntity;

        [SerializeField, Tooltip("生成開始距離(緑の範囲)")] float _spawnerEnableRange;

        [SerializeReference, SubclassSelector, Tooltip("生成パターン")]
        ISpawnPattern _spawnPattern;
        
        //現状はヒットしたplayerの参照をヒット時に格納する
        PlayerManager _playerManager;
        
        Transform _playerTransform;
        bool _spawnFlag;
        public ISpawnPattern SpawnPattern => _spawnPattern;
        
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            var position = transform.position;
            _spawnPattern.DrawGizmos(position);
            DrawWireDisk(position, _spawnerEnableRange, Color.green);

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
        
        void Start()
        {
            if (!GameObject.FindWithTag("Player").TryGetComponent(out _playerTransform))
            {
                Debug.LogWarning("Playerのタグが適切でない または、PlayerタグのオブジェクトにTransformがついていない");
            }
        }

        // TODO たぶんオブジェクトプールが必要になる気がする

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
                var entity = Instantiate(_fieldEntity[spawnIndex], transform);
                entity.transform.position = transform.position + pos;
                entity.SetPlayer(_playerManager);
                spawnIndex++;
            }
        }
        
        /// <summary>
        /// 2D円形のGizmosを描画するメソッド
        /// </summary>
        public static void DrawWireDisk(Vector3 position, float radius, Color color)
        {
            const float gizmoDiskThickness = 0.01f;
            // 参考 https://discussions.unity.com/t/draw-2d-circle-with-gizmos/123578/2
            var oldColor = Gizmos.color;
            Gizmos.color = color;
            var oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(position, Quaternion.identity, new Vector3(1, gizmoDiskThickness, 1));
            Gizmos.DrawWireSphere(Vector3.zero, radius);
            Gizmos.matrix = oldMatrix;
            Gizmos.color = oldColor;
        }
    }
}