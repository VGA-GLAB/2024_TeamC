using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using GraphProcessor;
using UnityEngine;
using Action = BehaviorTree.Action;

namespace SoulRunProject
{
    /// <summary>
    /// ボスの待機行動
    /// </summary>
    [Serializable, NodeMenuItem("Action/Boss/Test/Idle")]
    public class BossIdle : Action
    {
        public override void OnAwake()
        {
            
        }

        protected override void OnStart()
        {
            
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
