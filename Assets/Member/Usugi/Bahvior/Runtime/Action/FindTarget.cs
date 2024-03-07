using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// 目標を探す
    /// </summary>
    [Serializable, NodeMenuItem("Action/FindTarget")]
    public class FindTarget : Action
    {
        [Output("Target")] public GameObject Target;
        
        public override void OnAwake()
        {
  
        }

        protected override void OnStart()
        {
   
        }

        protected override BehavioreNodeState OnUpdate()
        {
            Target = GameObject.FindObjectOfType<TestTarget>().gameObject;
            var a = this.outputPorts.FirstOrDefault();
            if (Target == null)
            {
                return BehavioreNodeState.Failure;
            }
            return BehavioreNodeState.Success;
        }

        protected override void OnEnd()
        {
      
        }
    }
}
