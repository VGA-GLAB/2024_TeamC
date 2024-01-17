using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SoulRun.InGame
{
    public class CirclePlacementTool : EditorWindow
    {
        public GameObject objectPrefab;
        public Vector3 centerPosition = Vector3.zero;
        public float radius = 5.0f;
        public float spacing = 1.0f; // 新しい間隔パラメータ

        [MenuItem("Tools/Circle Placement Tool")]
        public static void ShowWindow()
        {
            GetWindow<CirclePlacementTool>("Circle Placement");
        }

        void OnGUI()
        {
            GUILayout.Label("Circle Placement Configuration", EditorStyles.boldLabel);

            objectPrefab = (GameObject)EditorGUILayout.ObjectField("Object Prefab", objectPrefab, typeof(GameObject), false);
            centerPosition = EditorGUILayout.Vector3Field("Center Position", centerPosition);
            radius = EditorGUILayout.FloatField("Radius", radius);
            spacing = EditorGUILayout.FloatField("Spacing", spacing); // 間隔の設定用UI

            if (GUILayout.Button("Place Objects"))
            {
                PlaceObjects();
            }
        }

        void PlaceObjects()
        {
            if (objectPrefab == null)
            {
                EditorUtility.DisplayDialog("Error", "No prefab selected for placement", "OK");
                return;
            }

            float circumference = 2 * Mathf.PI * radius;
            int numberOfObjects = Mathf.FloorToInt(circumference / spacing); // 円周に基づいてオブジェクトの数を計算

            for (int i = 0; i < numberOfObjects; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOfObjects;
                Vector3 position = centerPosition + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                Instantiate(objectPrefab, position, Quaternion.identity);
            }
        }
    }
}
