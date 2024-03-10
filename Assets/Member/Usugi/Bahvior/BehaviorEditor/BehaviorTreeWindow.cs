using System.IO;
using UnityEngine;
using GraphProcessor;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.Assertions;

namespace BehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーを表示するウィンドウクラス
    /// </summary>
    public class BehaviorTreeWindow : BaseGraphWindow
    {
        private BaseGraph _tmpGraph;
        
        [MenuItem("Window/01 BehaviorTree")]
        public static BaseGraphWindow OpenWithTmpGraph()
        {
            var window = CreateWindow<BehaviorTreeWindow>();
            window._tmpGraph = ScriptableObject.CreateInstance<BehaviorTreeGraph>();
            window._tmpGraph.hideFlags = HideFlags.HideAndDontSave;
            window.InitializeGraph(window._tmpGraph);
            window.Show();
            return window;
        }
        
        protected override void InitializeWindow(BaseGraph graph)
        {
            Assert.IsNotNull(graph);
            var fileName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(graph));
            titleContent = new GUIContent(ObjectNames.NicifyVariableName(fileName));
            if (graphView == null)
            {
                graphView = new DefaultBehaviorTree(this);
                graphView.Add(new BehaviorTreeCustomToolBarView(graphView));
            }
            rootView.Add(graphView);
        }

        protected override void OnDestroy()
        {
            graphView?.Dispose();
            
        }

        protected override void InitializeGraphView(BaseGraphView view)
        {
            //view.OpenPinned<ExposedParameterView>();
        }
    }
}
#endif

