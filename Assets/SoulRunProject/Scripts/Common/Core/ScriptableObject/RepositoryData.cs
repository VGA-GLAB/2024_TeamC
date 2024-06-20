using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SoulRunProject.Common
{
    [CreateAssetMenu(fileName = "Repository", menuName = "SoulRunProject/Repository")]
    public class RepositoryData : UnityEngine.ScriptableObject
    {
        [SerializeField] private List<string> _importPaths;
        [SerializeReference] private List<ObjectList> _dataList;
        
        public bool TryGetDataList<T>(out List<T> dataSet) where T : UnityEngine.Object
        {
            dataSet = new List<T>();
            foreach (var objectList in _dataList)
            {
                foreach (var objs in objectList.Objects)
                {
                    if (objs is T casted)
                    {
                        dataSet.Add(casted);
                    }
                }
            }
            return dataSet.Count > 0;
        }

        public bool TryGetData<T>(out T data) where T : UnityEngine.Object
        {
            data = null;
            foreach (var objectList in _dataList)
            {
                foreach (var objs in objectList.Objects)
                {
                    if (objs is T casted)
                    {
                        data = casted;
                        return true;
                    }
                }
            }
            return false;
        }

#if UNITY_EDITOR
        
        private void OnValidate()
        {
            _dataList.Clear();
            foreach (var path in _importPaths)
            {
                ObjectList data = new();

                var guids =  AssetDatabase.FindAssets("t:Object", new[] { path });
                var assetPaths = guids.Select(AssetDatabase.GUIDToAssetPath).ToArray();
                data.Objects = assetPaths.Select(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>).ToList();
                _dataList.Add(data);
            }
        }
#endif
        
        [Serializable]
        public class ObjectList
        {
            public List<UnityEngine.Object> Objects;
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(RepositoryData), true)]
    public class RepositoryEditor : Editor
    {
        
        private SerializedProperty _importPathsProperty;
        private SerializedProperty _dataListProperty;
        private int _levelUpListLastIndex;
        private void OnEnable()
        {
            _importPathsProperty = serializedObject.FindProperty("_importPaths");
            _dataListProperty = serializedObject.FindProperty("_dataList");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((UnityEngine.ScriptableObject)target) , typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.PropertyField(_importPathsProperty, new GUIContent("ロードするフォルダパス"), true);
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_dataListProperty, new GUIContent("参照データ"), true);
            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}