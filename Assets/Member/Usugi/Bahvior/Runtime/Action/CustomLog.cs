using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using SoulRunProject.Common;
using SoulRunProject.Framework;
using UnityEngine;

namespace BehaviorTree
{
    [Serializable, NodeMenuItem("Action/CustomLog")]
    public class CustomLog : Action
    {
        [SerializeField] private String _logMessage;
        
        public override void OnAwake()
        {
            
        }

        protected override void OnStart()
        {
            DebugClass.Instance.ShowLog("CustomLog: " + _logMessage);
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
