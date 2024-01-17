using Cysharp.Threading.Tasks;
using System;

namespace SoulRun.InGame
{
    /// <summary>
    /// インゲームの各ステートがもつインターフェース
    /// </summary>
    public interface IInGameState
    {
        public event Func<UniTask> OnStateStart;
        public event Func<UniTask> OnStateEnd;
        public event Action<IInGameState> OnStateExit;

        /// <summary>
        /// ステート遷移時、最初に一度だけ呼ばれる
        /// </summary>
        public void OnEnterState();

        /// <summary>
        /// そのステート中、Updateで呼ばれ続ける
        /// </summary>
        public void OnUpdateState();

        /// <summary>
        /// ステート切り替え時、一度だけ呼ばれる
        /// </summary>
        public void OnExitState();
    }
}
