using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SoulRun.InGame
{

    public class GridObjectPlacer : EditorWindow
    {
        public GameObject objectToPlace;
        public Vector3 startPosition = Vector3.zero;
        public int rows = 5;
        public int columns = 5;
        public float horizontalSpacing = 1.0f;
        public float verticalSpacing = 1.0f;

        [MenuItem("Window/Grid Object Placer")]
        public static void ShowWindow()
        {
            GetWindow<GridObjectPlacer>("Grid Object Placer");
        }

        void OnGUI()
        {
            GUILayout.Label("Grid Placement Settings", EditorStyles.boldLabel);

            objectToPlace = (GameObject)EditorGUILayout.ObjectField("Object to Place", objectToPlace, typeof(GameObject), true);
            startPosition = EditorGUILayout.Vector3Field("Start Position", startPosition);
            rows = EditorGUILayout.IntField("Rows", rows);
            columns = EditorGUILayout.IntField("Columns", columns);
            horizontalSpacing = EditorGUILayout.FloatField("Horizontal Spacing", horizontalSpacing);
            verticalSpacing = EditorGUILayout.FloatField("Vertical Spacing", verticalSpacing);

            if (GUILayout.Button("Place Objects"))
            {
                PlaceObjects();
            }
        }

        void PlaceObjects()
        {
            if (objectToPlace == null)
                return;

            Undo.RecordObject(this, "Place Objects");

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Vector3 position = new Vector3(startPosition.x + j * horizontalSpacing, startPosition.y, startPosition.z + i * verticalSpacing);
                    Instantiate(objectToPlace, position, Quaternion.identity);
                }
            }
        }
    }
}
