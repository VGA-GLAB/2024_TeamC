using System;
using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// targetが見つかっているかどうか
    /// </summary>
    [Serializable, NodeMenuItem("Condition/IsFindTarget")]
    public class IsFindTarget : Condition
    {
        [Input("Target")] public GameObject Target;
        public override void OnAwake()
        {
           
        }

        protected override void OnStart()
        {
     
        }

        protected override void OnEnd()
        {
          
        }

        public override BehavioreNodeState Evaluate()
        {
            if (Target == null)
            {
                return BehavioreNodeState.Failure;
            }
            return BehavioreNodeState.Success;
        }
    }
}
