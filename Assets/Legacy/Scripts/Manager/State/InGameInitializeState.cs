using Cysharp.Threading.Tasks;
using System;

namespace SoulRun.InGame
{
    /// <summary>
    /// インゲーム開始時に一度だけ呼ばれるステート
    /// スタートステートクラスに遷移する
    /// </summary>
    public class InGameInitializeState : IInGameState
    {
        public event Func<UniTask> OnStateStart;
        public event Func<UniTask> OnStateEnd;
        public event Action<IInGameState> OnStateExit;

        public void OnEnterState()
        {
            throw new NotImplementedException();
        }

        public void OnExitState()
        {
            throw new NotImplementedException();
        }

        public void OnUpdateState()
        {
            throw new NotImplementedException();
        }
    }
}
