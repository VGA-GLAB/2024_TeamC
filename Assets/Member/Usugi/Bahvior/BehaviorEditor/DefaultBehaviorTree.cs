using System;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
#if  UNITY_EDITOR
using UnityEditor;

namespace  BehaviorTree
{
    public class DefaultBehaviorTree : BaseGraphView
    {
        public DefaultBehaviorTree(EditorWindow window) : base(window)
        {
        }

        public override  IEnumerable<(string,Type)> FilterCreateNodeMenuEntries()
        {
            foreach (var nodeMenuItem in NodeProvider.GetNodeMenuEntries())
            {
                yield return nodeMenuItem;
            }
        }

        //protected override bool canDeleteSelection { get { return !selection.Any(e => e is Root); } }
    }
}
#endif
