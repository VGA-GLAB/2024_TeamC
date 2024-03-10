using System;
using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// 子ノードを順番に実行するノード
    /// 一つでも失敗したら失敗を返す
    /// 全て成功したら成功を返す
    /// </summary>
    [Serializable, NodeMenuItem("Composite/Sequencer")]
    public class Sequencer : Branch
    {
        protected override BehavioreNodeState OnUpdate()
        {
            if (_children == null || _children.Count == 0)
            {
                return BehavioreNodeState.Failure;
            }
            BehavioreNodeState state = _children[_currentChildIndex].Update();
            switch (state)
            {
                case BehavioreNodeState.Waiting:
                    break;
                case BehavioreNodeState.Success:
                    _currentChildIndex++;
                    if (_currentChildIndex >= _children.Count)
                    {
                        return BehavioreNodeState.Success;
                    }
                    break;
                case BehavioreNodeState.Failure:
                    return BehavioreNodeState.Failure;
                case BehavioreNodeState.Running:
                    break;
            }
            return BehavioreNodeState.Running;
        }
    }
}
