using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    
    public class ExposedPropertyGraphWindow : BaseGraphWindow
    {
        BaseGraph tmpGraph;
        
        [MenuItem("Window/04 ExposedPropertyGraphWindow")]
        public static BaseGraphWindow Open(BaseGraph graph)
        {
            var graphWindow = CreateWindow<ExposedPropertyGraphWindow>();
            graphWindow.tmpGraph = ScriptableObject.CreateInstance<ExposedPropertyGraphTest>();
            graphWindow.tmpGraph.hideFlags = HideFlags.HideAndDontSave;
            graphWindow.Show();
            return graphWindow;
        }

        protected override void OnDestroy()
        {
            graphView?.Dispose();
            DestroyImmediate(tmpGraph);
        }

        protected override void InitializeWindow(BaseGraph graph)
        {
            titleContent = new GUIContent("ExposedPropertyGraphWindow");
            if (graphView == null) 
                graphView = new ExposedPropertyGraphViewTest(this);
            rootView.Add(graphView);
        }

        protected override void InitializeGraphView(BaseGraphView view)
        {
            view.OpenPinned<ExposedParameterView>();
        }
    }
}
