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
        protected State _nextState;
        protected GameObject _owner;
        public void Enter()
        {
            OnEnter();
        }

        protected virtual void OnEnter() { }
        
        public async UniTask Enter(CancellationToken token)
        {
            await OnEnter(token);
        }
        
        protected virtual UniTask OnEnter(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }
        
        public void Exit()
        {
            OnExit();
        }
        
        protected virtual void OnExit() { }
        
        public void Update()
        {
            OnUpdate();
        }
        
        protected virtual void OnUpdate() { }
        
    }
}
