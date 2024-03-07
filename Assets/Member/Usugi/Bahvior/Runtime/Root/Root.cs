using System;
using System.Linq;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable, NodeMenuItem("Root")]
    public class Root : Node
    {
        [Output(name = "Child", allowMultiple = false), Vertical] public Node Child;

        public override void OnAwake()
        {
            if (Child == null) Child = GetOutputNodes().First() as Node;
        }

        protected override void OnStart()
        {
            if (Child == null) throw new Exception("Root node must have a child node");
        }

        protected override BehavioreNodeState OnUpdate()
        {
            State = Child.Update();
            return State;
        }

        protected override void OnEnd()
        {
            Debug.Log($"Reslt = {State}");
        }
    }
}
