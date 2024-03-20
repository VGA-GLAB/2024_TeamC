// TODO シーン上で生成パターンを見れるようにしたいね
// エディタ拡張の残滓

// using SoulRunProject.InGame;
// using UniRx;
// using UnityEngine;
//
// namespace SoulRunProject.Editor
// {
//     public class SpawnerEditor : MonoBehaviour
//     {
//         EntitySpawnController _entitySpawnController;
//         bool _flag;
//         const float GizmoDiskThickness = 0.01f;
//         
//         void Start()
//         {
//             _entitySpawnController = gameObject.GetComponent<EntitySpawnController>();
//             Initialize(_entitySpawnController);
//         }
//         
//         void Initialize(EntitySpawnController controller)
//         {
//             controller.ObserveEveryValueChanged(x => x.SpawnPattern)
//                 .Subscribe(value => { Debug.Log($"Change {value}"); })
//                 .AddTo(this);
//         }
//         
//         /// <summary>
//         /// スポナー起動範囲をシーン上に描画するメソッド
//         /// </summary>
//         void OnDrawGizmos()
//         {
//             DrawWireDisk(transform.position, _entitySpawnController.SpawnerEnableRange, Color.green);
//         }
//         
//         public static void DrawWireDisk(Vector3 position, float radius, Color color)
//         {
//             // 参考 https://discussions.unity.com/t/draw-2d-circle-with-gizmos/123578/2
//             var oldColor = Gizmos.color;
//             Gizmos.color = color;
//             var oldMatrix = Gizmos.matrix;
//             Gizmos.matrix = Matrix4x4.TRS(position, Quaternion.identity, new Vector3(1, GizmoDiskThickness, 1));
//             Gizmos.DrawWireSphere(Vector3.zero, radius);
//             Gizmos.matrix = oldMatrix;
//             Gizmos.color = oldColor;
//         }
//     }
// }