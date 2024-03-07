using System;
using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// ログを出力するノード
    /// </summary>
    [Serializable, NodeMenuItem("Action/Log")]
    public class Log : Action
    {
        [SerializeField, TextArea(1, 1)] private string _message;
        public override void OnAwake()
        {
            
        }

        protected override void OnStart()
        {
   
        }

        protected override BehavioreNodeState OnUpdate()
        {
            Debug.Log(_message);
            return BehavioreNodeState.Success;
        }

        protected override void OnEnd()
        {

        }
    }
}
