// TODO シーン上で生成パターンを見れるようにしたいね
// エディタ拡張の残滓

// using SoulRunProject.InGame;
// using UnityEditor;
// using UnityEngine;
//
// namespace SoulRunProject.Editor
// {
//     [CustomEditor(typeof(EntitySpawnController))]
//     public class SpawnerCustomEditor : UnityEditor.Editor
//     {
//         SerializedProperty _spawnPattern;
//
//         void OnEnable()
//         {
//             _spawnPattern = serializedObject.FindProperty(nameof(_spawnPattern));
//         }
//
//         public override void OnInspectorGUI()
//         {
//             base.OnInspectorGUI();
//
//             if (GUILayout.Button("Set Entity"))
//             {
//                 if (target is EntitySpawnController controller)
//                 {
//                     controller.SpawnEditorEntity();
//                 }
//             }
//         }
//     }
// }