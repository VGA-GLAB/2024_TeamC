using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// 条件判定を行うノードの基底クラス
    /// </summary>
    public abstract class Condition : Node
    {
        public abstract BehavioreNodeState Evaluate();
        protected override BehavioreNodeState OnUpdate()
        {
            State = Evaluate();
            return State;
        }
    }
}
