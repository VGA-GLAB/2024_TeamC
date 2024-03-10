using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace BehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーのインスペクター拡張
    /// </summary>
    [CustomEditor(typeof(BaseGraph), true)]
    public class BehaviorTreeAssetInspector : GraphInspector
    {
        protected override void CreateInspector()
        {
            base.CreateInspector();
            root.Add(new Button(() => EditorWindow.GetWindow<BehaviorTreeWindow>().InitializeGraph(target as BaseGraph))
            {
                text = "Open in BehaviorTreeWindow"
            });
        }
    }
}
