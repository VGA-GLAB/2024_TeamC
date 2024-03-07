using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    public class BehaviorTreeCustomToolBarView : ToolbarView
    {

        public BehaviorTreeCustomToolBarView(BaseGraphView graphView) : base(graphView)
        {
        }
        protected override void AddButtons()
        {
            base.AddButtons();
            // bool conditionalProcesserVisible = graphView.GetPinnedElementStatus<Conditional>()
        }
    }
}
