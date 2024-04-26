using System;
using System.Collections.Generic;
using System.Linq;
using SoulRunProject.SoulMixScene;
using UnityEditor;
using UnityEngine;

namespace SoulRunProject.Common.Core.ScriptableObject
{
    [CreateAssetMenu(fileName = "Repository", menuName = "SoulRunProject/Repository")]
    public class RepositoryData : UnityEngine.ScriptableObject
    {
        [SerializeField] private List<string> _importPaths;
        [SerializeReference] private List<ScriptableObjectList> _dataList;
        
        public bool TryGetDataList<T>(out List<T> dataSet) where T : UnityEngine.ScriptableObject
        {
            dataSet = null;
            foreach (var scriptableObjectList in _dataList)
            {
                if (scriptableObjectList.ScriptableObjects[0] is T)
                {
                    dataSet = scriptableObjectList.ScriptableObjects.Select(x => x as T).ToList();
                }
            }
            return dataSet != null;
        }

        public bool TryGetData<T>(out T data) where T : UnityEngine.ScriptableObject
        {
            data = null;
            foreach (var scriptableObjectList in _dataList)
            {
                if (scriptableObjectList.ScriptableObjects[0] is T casted)
                {
                    data = casted;
                }
            }
            return data != null;
        }
        
        private void OnValidate()
        {
            _dataList.Clear();
            foreach (var path in _importPaths)
            {
                if (path == String.Empty) continue;
                ScriptableObjectList data = new();
                var guids =  AssetDatabase.FindAssets("t:scriptableObject", new[] { path });
                if (guids.Length == 0) continue;
                var assetPaths = guids.Select(AssetDatabase.GUIDToAssetPath).ToArray();
                data.ScriptableObjects = assetPaths.Select(AssetDatabase.LoadAssetAtPath<UnityEngine.ScriptableObject>).ToList();
                if (data.ScriptableObjects.Count > 0)
                {
                    _dataList.Add(data);
                }
            }
        }
        
        [Serializable]
        public class ScriptableObjectList
        {
            public List<UnityEngine.ScriptableObject> ScriptableObjects;
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
            
            // if (GUILayout.Button("DebugData"))
            // {
            //     SoulRunRepository script = target as SoulRunRepository;
            //     if(script.TryGetDataList<SkillBase>(out var dataSet));
            //     {
            //         Debug.Log($"{string.Join("\n" , dataSet)}");
            //     }
            // }
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(_dataListProperty, new GUIContent("参照データ"), true);
            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}