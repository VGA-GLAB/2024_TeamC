using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class SceneFileManager : EditorWindow
{
    private Vector2 scrollPosition;
    private List<string> sceneFiles;

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
    }
}