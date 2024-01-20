using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class SceneFileManager : EditorWindow
{
    private Vector2 scrollPosition; // スクロール位置
    private List<string> sceneFiles; // シーンファイルのリスト

    private string newSceneName = "New Scene"; // 新しく作成するシーンの名前
    private string controlScenePath = "Assets/Scenes/"; // 新しく作成するシーンのパス
    private string generatedFolderPath = "Assets/Project"; // 生成、管理するファイルのパス
    private string generatedFolderName = "Project"; // 生成、管理するファイルの名前

    [MenuItem("HikanyanTools/Scene File Manager")]
    public static void ShowWindow()
    {
        GetWindow<SceneFileManager>("Scene File Manager");
    }

    void OnEnable()
    {
        sceneFiles = new List<string>();
        var guids = AssetDatabase.FindAssets("t:Scene");
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!path.EndsWith("/Basic.unity") && !path.EndsWith("/Standard.unity"))
            {
                sceneFiles.Add(path);
            }
        }
    }

    void OnGUI()
    {
        GUILayout.Label("Scene Files", EditorStyles.boldLabel);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (string sceneFile in sceneFiles)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(Path.GetFileName(sceneFile), GUILayout.Width(200));

            if (GUILayout.Button("Open"))
            {
                EditorSceneManager.OpenScene(sceneFile);
            }

            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(sceneFile);
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
        generatedFolderPath = EditorGUILayout.TextField("Folder Path", generatedFolderPath);
        generatedFolderName = EditorGUILayout.TextField("Folder Name", generatedFolderName);

        if (GUILayout.Button("Generate Structure"))
        {
            GenerateProjectFileStructure();
        }
    }

    private void GenerateProjectFileStructure()
    {
        string fullPath = Path.Combine(generatedFolderPath, generatedFolderName);
        ProjectFileStructureGenerator.CreateSceneFolders(fullPath);
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
            Debug.LogError("Scene already exists: " + scenePath);
        }
    }
}
