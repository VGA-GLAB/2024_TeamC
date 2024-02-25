using Cysharp.Threading.Tasks;
using System.Threading;

namespace SoulRunProject.Common
{
    /// <summary>
    /// Stateの基底クラス
    /// </summary>
    public abstract class State
    {
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
        
        public async UniTask Exit(CancellationToken token)
        {
            await OnExit(token);
        }
        
        protected virtual UniTask OnExit(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }
        
        public void Update()
        {
            OnUpdate();
        }
        
        protected virtual void OnUpdate() { }
        
        public async UniTask Update(CancellationToken token)
        {
            await OnUpdate(token);
        }
        
        protected virtual UniTask OnUpdate(CancellationToken token)
        {
            return UniTask.CompletedTask;
        }

        
    }
}
