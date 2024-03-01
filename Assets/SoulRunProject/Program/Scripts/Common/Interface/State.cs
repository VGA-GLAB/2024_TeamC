using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// Stateの基底クラス
    /// 非同期と同期用のStart,Update,Exit関数を持つ
    /// 今回はMonobehaviorを使用し、インスペクターから設定できるように
    /// </summary>
    public abstract class State
    {
        protected Dictionary<int,State> _nextStates;
        protected GameObject _owner;
        public void Enter(State currentState)
        {
            OnEnter(currentState);
        }

        protected virtual void OnEnter(State currentState) { }
        
        public async UniTask Enter(State currentState, CancellationToken token)
        {
            await OnEnter(currentState, token);
        }
        
        protected virtual UniTask OnEnter(State currentState, CancellationToken token)
        {
            return UniTask.CompletedTask;
        }
        
        public void Exit(State nextState)
        {
            OnExit(nextState);
        }
        
        protected virtual void OnExit(State nextState) { }
        
        public void Update()
        {
            OnUpdate();
        }
        
        protected virtual void OnUpdate() { }
        
    }
}
