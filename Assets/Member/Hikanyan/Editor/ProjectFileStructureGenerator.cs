using UnityEngine;
using UnityEditor;
using System.IO;

namespace SoulRunProject.Editor
{
    public class ProjectFileStructureGenerator
    {
        /// <summary>
        /// 指定されたファイルパスとシーン名に基づいて、プロジェクトのフォルダ構造を生成します。
        /// </summary>
        /// <param name="filePath">プロジェクトのファイルの名前を含むパス</param>
        /// <param name="sceneName">シーン名</param>
        public static void CreateSceneFolders(string filePath, string sceneName)
        {
            string[] folderPaths = new string[]
            {
                $"Assets/{filePath}/Audio/{sceneName}",
                $"Assets/{filePath}/Design/UI/{sceneName}",
                $"Assets/{filePath}/Design/Materials/{sceneName}",
                $"Assets/{filePath}/GameData/Prefabs/Data/{sceneName}",
                $"Assets/{filePath}/GameData/Prefabs/Systems/{sceneName}",
                $"Assets/{filePath}/GameData/Prefabs/UI/{sceneName}",
                $"Assets/{filePath}/GameData/ScriptableObjects/{sceneName}",
                $"Assets/{filePath}/Program/Scripts/Common/Core",
                $"Assets/{filePath}/Program/Scripts/Common/Interface",
                $"Assets/{filePath}/Program/Scripts/Common/UI",
                $"Assets/{filePath}/Program/Scripts/{sceneName}/Core",
                $"Assets/{filePath}/Program/Scripts/{sceneName}/Interface",
                $"Assets/{filePath}/Program/Scripts/{sceneName}/UI",
                $"Assets/{filePath}/Program/Shaders/{sceneName}",
                $"Assets/{filePath}/Scenes/DebugScene/{sceneName}",
                $"Assets/{filePath}/Scenes/MainScene/{sceneName}",
                $"Assets/AssetStoreTools",
                $"Assets/Editor",
                $"Assets/Resources",
                $"Assets/StreamingAssets",
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
}