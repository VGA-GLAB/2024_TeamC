using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable, NodeMenuItem("Action/CustomLog")]
    public class CustomLog : Action
    {
        [Input("Target")] public GameObject Target;
        
        public override void OnAwake()
        {
            
        }

        protected override void OnStart()
        {
            Debug.Log(Target.name);
        }

        protected override BehavioreNodeState OnUpdate()
        {
            return BehavioreNodeState.Success;
        }

        protected override void OnEnd()
        {
            
        }
    }
}
