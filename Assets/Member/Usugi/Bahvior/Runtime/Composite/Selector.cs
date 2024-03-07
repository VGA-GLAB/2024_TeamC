using System;
using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// 一つでも成功したら成功を返す
    /// すべて失敗したら失敗を返す
    /// </summary>
    [Serializable, NodeMenuItem("Composite/Selector")]
    public class Selector : Branch
    {
        protected override BehavioreNodeState OnUpdate()
        {
            foreach(var node in _children)
            {
                switch (node.Update())
                {
                    case BehavioreNodeState.Running:
                        return BehavioreNodeState.Running;
                    case BehavioreNodeState.Success:
                        return BehavioreNodeState.Success;
                    case BehavioreNodeState.Failure:
                        continue;
                }
            }

            return BehavioreNodeState.Failure;
        }
    }
}
