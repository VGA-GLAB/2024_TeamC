using SoulRunProject.Common;
using SoulRunProject.InGame;
using SoulRunProject.Framework;
using System;
using UniRx;

namespace SoulRunProject
{
    public class PauseState : State
    {
        private readonly PlayerManager _playerManager;
        private readonly PlayerInput _playerInput;
        private IDisposable _disposable;
        
        public PauseState(PlayerManager playerManager, PlayerInput playerInput)
        {
            _playerManager = playerManager;
            _playerInput = playerInput;
        }

        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ポーズステート開始");
            _playerManager.SwitchPause(true);
            
            // PlayerInputへの購読
            _disposable = _playerInput.PauseInput
                .SkipLatestValueOnSubscribe()
                .Where(x => x)
                .Subscribe( _ =>
                {
                    StateChange();
                });
        }

        protected override void OnExit(State nextState)
        {
            _disposable.Dispose();
        }
    }
}
