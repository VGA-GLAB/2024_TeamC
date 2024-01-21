using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

namespace SoulRunProject.Editor
{
    public class SceneFileManager : EditorWindow
    {
        //
        private class SceneInfo
        {
            public string Path;
            public string Name;
            public bool GenerateFolder;

            public SceneInfo(string path, string name)
            {
                Path = path;
                Name = name;
                GenerateFolder = true;
            }
        }

        private Vector2 scrollPosition;
        private List<SceneInfo> sceneInfos;

        private string _newSceneName = "New Scene";
        private string _controlScenePath = "Assets/Scenes/";
        private string _generatedFolderName = "Project";

        private string _forcasname = "";
        string _lastName = "";
        private bool _isNameChanged = false;

        [MenuItem("HikanyanTools/Scene File Manager")]
        public static void ShowWindow()
        {
            GetWindow<SceneFileManager>("Scene File Manager");
        }

        void OnEnable()
        {
            sceneInfos = new List<SceneInfo>();
            var guids = AssetDatabase.FindAssets("t:Scene");
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (!path.EndsWith("/Basic.unity") && !path.EndsWith("/Standard.unity"))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                    sceneInfos.Add(new SceneInfo(path, fileNameWithoutExtension));
                }
            }
        }

        void OnGUI()
        {
            GUILayout.Label("Scene Files", EditorStyles.boldLabel);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            foreach (var sceneInfo in sceneInfos)
            {
                GUILayout.BeginHorizontal();
                sceneInfo.GenerateFolder = EditorGUILayout.Toggle(sceneInfo.GenerateFolder, GUILayout.Width(20));

                EditorGUI.BeginChangeCheck(); // テキストフィールドの変更をチェック開始
                GUI.SetNextControlName(sceneInfo.Name);

                string newName = EditorGUILayout.TextField(sceneInfo.Name, GUILayout.Width(200));
                if (!_isNameChanged)
                {
                    if (newName == _lastName)
                    {
                        _lastName = newName;
                        _isNameChanged = true;
                    }

                    Debug.Log(_lastName);
                }

                bool nameChanged = EditorGUI.EndChangeCheck(); // テキストフィールドの変更をチェック終了
                // エンターキー入力判定
                if (_forcasname != GUI.GetNameOfFocusedControl())
                {
                    Debug.Log("変わった");
                    RenameScene(sceneInfo, newName);

                    _isNameChanged = false;
                }

                if (GUILayout.Button("Open"))
                {
                    EditorSceneManager.OpenScene(sceneInfo.Path);
                }

                if (GUILayout.Button("Select"))
                {
                    Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(sceneInfo.Path);
                }

                GUILayout.EndHorizontal();
            }


            GUILayout.EndScrollView();

            GUILayout.Space(10);
            GUILayout.Label("Create New Scene", EditorStyles.boldLabel);
            _newSceneName = EditorGUILayout.TextField("Scene Name", _newSceneName);
            _controlScenePath = EditorGUILayout.TextField("Scene Path", _controlScenePath);
            Debug.Log(GUI.GetNameOfFocusedControl());
            if (GUILayout.Button("Create New Scene"))
            {
                CreateNewScene();
            }

            GUILayout.Space(10);
            GUILayout.Label("Generate Project File Structure", EditorStyles.boldLabel);
            _generatedFolderName = EditorGUILayout.TextField("Folder Name", _generatedFolderName);

            if (GUILayout.Button("Generate Structure"))
            {
                GenerateProjectFileStructure();
            }

            _forcasname = GUI.GetNameOfFocusedControl();
        }

        private void GenerateProjectFileStructure()
        {
            foreach (var sceneInfo in sceneInfos)
            {
                if (sceneInfo.GenerateFolder)
                {
                    ProjectFileStructureGenerator.CreateSceneFolders(_generatedFolderName, sceneInfo.Name);
                }
            }
        }

        private void CreateNewScene()
        {
            string scenePath = Path.Combine(_controlScenePath, _newSceneName + ".unity");
            if (!File.Exists(scenePath))
            {
                EditorSceneManager.SaveScene(EditorSceneManager.NewScene(NewSceneSetup.EmptyScene), scenePath);
                AssetDatabase.Refresh();
                Debug.Log($"New scene created: {scenePath}");
            }
            else
            {
                Debug.LogError($"Scene already exists: {scenePath}");
            }
        }

        private void RenameScene(SceneInfo sceneInfo, string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                Debug.LogError("Error: The new scene name cannot be empty.");
                return;
            }

            string newScenePath = Path.Combine(Path.GetDirectoryName(sceneInfo.Path), newName + ".unity");

            if (File.Exists(newScenePath))
            {
                Debug.LogError($"Error: A scene with the name '{newName}' already exists.");
                return;
            }

            string error = AssetDatabase.RenameAsset(sceneInfo.Path, newName);
            if (string.IsNullOrEmpty(error))
            {
                sceneInfo.Name = newName;
                sceneInfo.Path = newScenePath;
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log($"Scene renamed to: {newScenePath}");
            }
            else
            {
                Debug.LogError($"Error: Failed to rename the scene '{sceneInfo.Name}' to '{newName}'. Error: {error}");
            }
        }
    }
}
