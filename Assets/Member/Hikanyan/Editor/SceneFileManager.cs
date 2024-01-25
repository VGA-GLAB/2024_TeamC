using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

namespace SoulRunProject.Editor
{
    public class SceneFileManager : EditorWindow
    {
        /// <summary> シーンファイルの情報を保持するクラス </summary>
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

            /// <summary> シーン名を変更します。 </summary>
            public void Rename(string newName)
            {
                if (string.IsNullOrEmpty(newName))
                {
                    Debug.LogError("Error: The new scene name cannot be empty.");
                    return;
                }

                string newScenePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Path), newName + ".unity");

                if (File.Exists(newScenePath))
                {
                    Debug.LogError($"Error: A scene with the name '{newName}' already exists.");
                    return;
                }

                string error = AssetDatabase.RenameAsset(Path, newName);
                if (string.IsNullOrEmpty(error))
                {
                    Name = newName;
                    Path = newScenePath;
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    Debug.Log($"Scene renamed to: {newScenePath}");
                }
                else
                {
                    Debug.LogError($"Error: Failed to rename the scene '{Name}' to '{newName}'. Error: {error}");
                }
            }
        }

        private Vector2 _scrollPosition;
        private List<SceneInfo> _sceneInfos;

        private string _newSceneName = "New Scene";
        private string _controlScenePath = "Assets/Scenes/";
        private string _generatedFolderName = "Project";

        /// <summary> ウィンドウを開きます。 </summary>
        [MenuItem("HikanyanTools/Scene File Manager")]
        public static void ShowWindow()
        {
            GetWindow<SceneFileManager>("Scene File Manager");
        }

        /// <summary> ウィンドウが開かれたときに呼び出されます。 </summary>
        void OnEnable()
        {
            _sceneInfos = new List<SceneInfo>();
            var guids = AssetDatabase.FindAssets("t:Scene");
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (!path.EndsWith("/Basic.unity") && !path.EndsWith("/Standard.unity"))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                    _sceneInfos.Add(new SceneInfo(path, fileNameWithoutExtension));
                }
            }
        }

        /// <summary> ウィンドウのGUIを描画します。 </summary>
        void OnGUI()
        {
            DrawSceneFilesSection();
            DrawCreateNewSceneSection();
            DrawGenerateProjectFileStructureSection();
        }


        /// <summary> シーンファイルの情報を描画します。 </summary>
        private void DrawSceneFilesSection()
        {
            GUILayout.Label("Scene Files", EditorStyles.boldLabel);
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            foreach (var sceneInfo in _sceneInfos)
            {
                DrawSceneInfoRow(sceneInfo);
            }

            GUILayout.EndScrollView();
        }

        /// <summary> シーンファイルの情報を描画します。 </summary>
        private void DrawSceneInfoRow(SceneInfo sceneInfo)
        {
            GUILayout.BeginHorizontal();
            sceneInfo.GenerateFolder = EditorGUILayout.Toggle(sceneInfo.GenerateFolder, GUILayout.Width(20));

            // Enterキーが押されたかどうかを確認
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
            {
                if (GUI.GetNameOfFocusedControl() == sceneInfo.Path)
                {
                    // ここでシーン名を更新
                    sceneInfo.Rename(GUI.TextField(new Rect(), sceneInfo.Name));
                    GUI.FocusControl(null); // フォーカスをクリア
                    return; // イベントを処理済みとして終了
                }
            }

            // シーン名を編集するテキストフィールド
            GUI.SetNextControlName(sceneInfo.Path);
            sceneInfo.Name = EditorGUILayout.TextField(sceneInfo.Name, GUILayout.Width(200));

            // シーンを開くボタン
            if (GUILayout.Button("Open"))
            {
                EditorSceneManager.OpenScene(sceneInfo.Path);
            }

            // シーンを選択するボタン
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(sceneInfo.Path);
            }

            GUILayout.EndHorizontal();
        }



        /// <summary> 新しいシーンを作成するセクションを描画します。 </summary>
        private void DrawCreateNewSceneSection()
        {
            GUILayout.Space(10);
            GUILayout.Label("Create New Scene", EditorStyles.boldLabel);
            _newSceneName = EditorGUILayout.TextField("Scene Name", _newSceneName);
            _controlScenePath = EditorGUILayout.TextField("Scene Path", _controlScenePath);

            // ボタンが押されたら新しいシーンを作成
            if (GUILayout.Button("Create New Scene"))
            {
                CreateNewScene();
            }
        }

        /// <summary> プロジェクトのファイル構造を生成するセクションを描画します。 </summary>
        private void DrawGenerateProjectFileStructureSection()
        {
            GUILayout.Space(10);
            GUILayout.Label("Generate Project File Structure", EditorStyles.boldLabel);
            _generatedFolderName = EditorGUILayout.TextField("Folder Name", _generatedFolderName);

            if (GUILayout.Button("Generate Structure"))
            {
                GenerateProjectFileStructure();
            }
        }


        /// <summary> プロジェクトのファイル構造を生成します。 </summary>
        private void GenerateProjectFileStructure()
        {
            foreach (var sceneInfo in _sceneInfos)
            {
                if (sceneInfo.GenerateFolder)
                {
                    ProjectFileStructureGenerator.CreateSceneFolders(_generatedFolderName, sceneInfo.Name);
                }
            }
        }

        /// <summary> 新しいシーンを作成します。 </summary>
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
    }
}