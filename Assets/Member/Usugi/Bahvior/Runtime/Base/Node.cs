using GraphProcessor;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// ノードの状態
    /// </summary>
    public enum BehavioreNodeState
    {
        Waiting,
        Success,
        Failure,
        Running,
    }
    /// <summary>
    /// BaseNodeを継承
    /// 親ノードの参照、名前、説明、状態を持つ
    /// 状態の変更はアップデートのみで行い、開始終了処理はただ処理を行うだけ(状態を変えない)
    /// </summary>
    public abstract class Node : BaseNode
    {
        [Input(name = "Parent"), Vertical]public Node Parent;
        private string Description;
        public BehavioreNodeState State = BehavioreNodeState.Waiting;
        private GameObject _owner;
        
        /// <summary>
        /// Nodeの親を設定する
        /// </summary>
        /// <param name="owner"></param>
        public void SetOwner(GameObject owner)
        {
            _owner = owner;
        }
        
        public BehavioreNodeState Update()
        {
            if (State == BehavioreNodeState.Waiting)
            {   //待機状態なら開始処理を行い、実行状態にする
                OnStart();
                State = BehavioreNodeState.Running;
            }
            State = OnUpdate();
            if (State == BehavioreNodeState.Success || State == BehavioreNodeState.Failure)
            {  //成功か失敗なら終了処理を行い、結果を返す
                OnEnd();
                return State;
            }
            return State;
        }
        
        /// <summary>
        /// こちらはビヘイビアツリー起動時に1度だけ呼ばれる
        /// </summary>
        public abstract void OnAwake();
        /// <summary>
        /// こちらはステートがWaitingの時に1度だけ呼ばれる
        /// </summary>
        protected abstract void OnStart();
        protected abstract BehavioreNodeState OnUpdate();
        protected abstract void OnEnd();
    }
}
