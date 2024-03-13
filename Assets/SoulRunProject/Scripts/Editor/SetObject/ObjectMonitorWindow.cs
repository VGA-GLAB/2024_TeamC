using UnityEngine;
using UnityEditor;

namespace SoulRun.InGame
{


    public class ObjectMonitorWindow : EditorWindow
    {
        private Transform monitoredObject;
        private Vector3 previousPosition;

        // ウィンドウを表示するためのメニューアイテムを追加
        [MenuItem("Tools/Object Monitor")]
        public static void ShowWindow()
        {
            GetWindow<ObjectMonitorWindow>("Object Monitor");
        }

        void OnGUI()
        {
            // 監視するオブジェクトの選択フィールド
            monitoredObject = EditorGUILayout.ObjectField("Monitored Object", monitoredObject, typeof(Transform), true) as Transform;

            // 監視を開始するためのボタン（オプション）
            if (GUILayout.Button("Start Monitoring"))
            {
                StartMonitoring();
            }
        }

        private void StartMonitoring()
        {
            if (monitoredObject != null)
            {
                previousPosition = monitoredObject.position;
                EditorApplication.update += OnUpdate;
            }
        }

        void OnUpdate()
        {
            if (monitoredObject && monitoredObject.position != previousPosition)
            {
                Debug.Log("Position changed: " + monitoredObject.position);
                previousPosition = monitoredObject.position;
            }
        }

        void OnDisable()
        {
            // ウィンドウが閉じられたときに更新イベントからメソッドを削除
            EditorApplication.update -= OnUpdate;
        }
    }

}
