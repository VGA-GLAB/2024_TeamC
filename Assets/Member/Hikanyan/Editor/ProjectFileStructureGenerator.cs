using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class ProjectFileStructureGenerator
{
    static ProjectFileStructureGenerator()
    {
        EditorApplication.hierarchyChanged += CheckAndCreateSceneFolders;
    }

    private static void CheckAndCreateSceneFolders()
    {
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string sceneName = Path.GetFileNameWithoutExtension(scene.path);
                CreateSceneFolders(sceneName);
            }
        }
    }

    private static void CreateSceneFolders(string sceneName)
    {
        string[] folderPaths = new string[]
        {
            $"Assets/Audio/{sceneName}",
            $"Assets/Design/UI/{sceneName}",
            $"Assets/Design/Materials/{sceneName}",
            $"Assets/GameData/Prefabs/Data/{sceneName}",
            $"Assets/GameData/Prefabs/Systems/{sceneName}",
            $"Assets/GameData/Prefabs/UI/{sceneName}",
            $"Assets/GameData/ScriptableObjects/{sceneName}",
            $"Assets/Program/Scripts/{sceneName}",
            $"Assets/Program/Shaders/{sceneName}"
        };

        foreach (var path in folderPaths)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Debug.Log($"Created folder: {path}");
            }
        }

        AssetDatabase.Refresh();
    }
}