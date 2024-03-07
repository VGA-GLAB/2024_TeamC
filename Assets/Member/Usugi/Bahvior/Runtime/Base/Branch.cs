using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphProcessor;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// 子を持つことのできるノードの基底クラス
    /// 開始時と終了時に実行する子ノードのインデックスの初期化を行う
    /// </summary>
    public abstract class Branch : Node
    {
        [Output(name = "Children", allowMultiple = true), Vertical] public Node Children;
        protected List<Node> _children = new List<Node>();
        protected int _currentChildIndex = 0;

        public override void OnAwake()
        {
            //　子ノードを取得し、左から順番に子ノードのリストへ格納
            var outputNodes = GetOutputNodes().OrderBy(x => x.position.x);
            foreach (var outputNode in outputNodes)
            {
                if (outputNode is Node)
                {
                    _children.Add(outputNode as Node);
                }
            }
        }
        
        protected override void OnStart()
        {
            _currentChildIndex = 0;
        }

        protected override void OnEnd()
        {
            _currentChildIndex = 0;
        }
    }
}
