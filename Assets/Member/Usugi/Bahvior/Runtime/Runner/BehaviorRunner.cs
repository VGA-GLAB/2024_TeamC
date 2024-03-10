using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// ビヘイビアツリーを実行するクラス
    /// 実行はコルーチンで行う
    /// </summary>
    public class BehaviorRunner : MonoBehaviour
    {
        [SerializeField] private BehaviorTreeGraph _graph;
        private Node _root;

        /// <summary>
        /// 外部からビヘイビアツリーを呼び出す処理
        /// 一度呼んでいる場合は初期化処理は行わない
        /// </summary>
        public void RunTree()
        {
            _graph = _graph.Clone();
            SetRoot();
            SetDefaultNodeProcess();
            SetEveryNodeParent();
            CallOnAwake();
            StartCoroutine(UpdateTree());
        }

        /// <summary>
        /// 自作のノード以外の初期化処理を行う
        /// </summary>
        private void SetDefaultNodeProcess()
        {
            foreach (var node in _graph.nodes)
            {
                node.OnProcess();
            }
        }

        /// <summary>
        /// 全ノードの親を設定する
        /// </summary>
        private void SetEveryNodeParent()
        {
            foreach (var node in _graph.nodes)
            {
                //TODO: ここでどこにもつながってないノードがあるとInvalidエラー
                var behavioreNode = node as Node;
                if (IsNotBehaviorNode(behavioreNode)) continue;
                if (behavioreNode is Root) continue;
                if (behavioreNode != null) behavioreNode.Parent = behavioreNode.GetInputNodes().First() as Node;
            }
        }

        private static bool IsNotBehaviorNode(Node behavioreNode)
        {
            if (behavioreNode is not Node)
            {
                // Rootノードとノード以外のものは親を持たない
                return true;
            }

            return false;
        }

        /// <summary>
        /// 全ノードのAwake処理を行う。これは一度しか呼ばれない
        /// </summary>
        private void CallOnAwake()
        {
            foreach (var node in _graph.nodes)
            {
                var behavioreNode = node as Node;
                if (IsNotBehaviorNode(behavioreNode)) continue;
                behavioreNode.OnAwake();
            }
        }

        private void SetRoot()
        {
            _root = _graph.nodes.Find(n => n is Root) as Root;
        }

        private int test = 0;

        /// <summary>
        /// 実際にビヘイビアツリーを実行する内部関数
        /// </summary>
        private IEnumerator UpdateTree()
        {
            foreach (var node in _graph.nodes)
            {
                var treeNode = node as Node;
                if (IsNotBehaviorNode(treeNode)) continue;
                treeNode.State = BehavioreNodeState.Waiting;
            }
            Stack<Node> stack = new Stack<Node>();
            stack.Push(_root);
            while (stack.Count > 0)
            {
                var node = stack.Peek();
                switch (node.Update())
                {
                    case BehavioreNodeState.Success:
                        stack.Pop();
                        break;
                    case BehavioreNodeState.Failure:
                        stack.Pop();
                        break;
                    case BehavioreNodeState.Running:
                        break;
                }

                yield return null;
            }
        }
    }
}
