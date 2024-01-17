using UnityEditor;
using UnityEngine;

namespace SoulRun.InGame.Editor
{
    /// <summary>
    /// 指定したPrefabを指定した配置するエディタ拡張
    /// </summary>
    public class PrefabSpawnerEditor : EditorWindow
    {
        /// <summary> 生成するPrefab </summary>
        private GameObject _prefab;
        /// <summary> 生成する数 </summary>
        private int _count = 5;
        /// <summary> 生成するオブジェクトの間隔 </summary>
        private float _spacing = 0f;
        /// <summary> 生成開始地点 </summary>
        private Vector3 _startPos = Vector3.zero;

        [MenuItem("Tools/Spawn Prefabs")]
        static void ShowWindow()
        {   //この機能のWindowを表示
            GetWindow<PrefabSpawnerEditor>("Prefab Spawner");
        }

        private void OnGUI()
        {
            GUILayout.Label("Prefab Spawner", EditorStyles.boldLabel);

            //Prefab、生成数、間隔を指定する場所を表示する
            _prefab = EditorGUILayout.ObjectField("Prefab", _prefab, typeof(GameObject), false) as GameObject;
            _count = EditorGUILayout.IntField("Count", _count);
            _spacing = EditorGUILayout.FloatField("Spacing", _spacing);
            _startPos = EditorGUILayout.Vector3Field("StartPos", _startPos);

            if (GUILayout.Button("Spawn Prefabs"))
            {   //生成
                SpawnPrefabs();
            }
        }

        /// <summary>
        /// Fieldから設定した値でPrefabを生成する
        /// </summary>
        private void SpawnPrefabs()
        {
            if (_prefab == null)
            {   //Prefabが設定してないなら警告
                Debug.LogError("Prefab not assigned!");
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                GameObject obj = PrefabUtility.InstantiatePrefab(_prefab) as GameObject;
                if (obj != null)
                {
                    obj.transform.position = new Vector3(_startPos.x, _startPos.y, _startPos.z + _spacing * i);
                    Undo.RegisterCreatedObjectUndo(obj, "Spawn Prefabs");
                }
            }
        }
    }
}
