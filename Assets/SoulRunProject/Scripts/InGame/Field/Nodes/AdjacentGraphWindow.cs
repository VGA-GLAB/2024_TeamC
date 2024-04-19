using System.IO;
using UnityEngine;
using GraphProcessor;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Assertions;

namespace SoulRunProject.Common
{
    public class AdjacentGraphWindow : BaseGraphWindow
    {
        protected override void InitializeWindow(BaseGraph graph)
        {
            Assert.IsNotNull(graph);
        
            // ウィンドウのタイトルを適当に設定
            var fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(graph));
            titleContent = new GUIContent(ObjectNames.NicifyVariableName(fileName));
            // グラフを編集するためのビューであるGraphViewを設定
            if (graphView == null)
            {
                graphView = new BaseGraphView(this);
            }
            rootView.Add(graphView);
        }
    }
}
#endif