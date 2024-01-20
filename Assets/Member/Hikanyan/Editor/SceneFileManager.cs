using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class SceneFileManager : EditorWindow
{
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

    private string newSceneName = "New Scene";
    private string controlScenePath = "Assets/Scenes/";
    private string generatedFolderName = "Project";

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
            string newName = EditorGUILayout.TextField(sceneInfo.Name, GUILayout.Width(200));
            bool nameChanged = EditorGUI.EndChangeCheck(); // テキストフィールドの変更をチェック終了

            if (nameChanged)
            {
                RenameScene(sceneInfo, newName);
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
        newSceneName = EditorGUILayout.TextField("Scene Name", newSceneName);
        controlScenePath = EditorGUILayout.TextField("Scene Path", controlScenePath);

        if (GUILayout.Button("Create New Scene"))
        {
            CreateNewScene();
        }

        GUILayout.Space(10);
        GUILayout.Label("Generate Project File Structure", EditorStyles.boldLabel);
        generatedFolderName = EditorGUILayout.TextField("Folder Name", generatedFolderName);

        if (GUILayout.Button("Generate Structure"))
        {
            GenerateProjectFileStructure();
        }
    }

    private void GenerateProjectFileStructure()
    {
        foreach (var sceneInfo in sceneInfos)
        {
            if (sceneInfo.GenerateFolder)
            {
                ProjectFileStructureGenerator.CreateSceneFolders(generatedFolderName, sceneInfo.Name);
            }
        }
    }

    private void CreateNewScene()
    {
        string scenePath = Path.Combine(controlScenePath, newSceneName + ".unity");
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