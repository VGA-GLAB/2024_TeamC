using System;
using System.Threading;
using SoulRunProject.Common;
using SoulRunProject.Framework;
using UniRx;
using Cysharp.Threading.Tasks;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ランゲームプレイ中の管理を行うクラス
    /// </summary>
    public class PlayingRunGameState : State
    {
        private PlayerManager _playerManager;
        private PlayerInput _playerInput;
        private IDisposable _disposable;
        
        //TODO：ボスステージ開始前のプレイヤーの位置を設定する場所を検討
        private float _enterBossStagePosition = 1000f;
        public bool ArrivedBossStagePosition { get; private set; } = false;
        public bool SwitchToPauseState { get; private set; } = false;
        
        public PlayingRunGameState(PlayerManager playerManager, PlayerInput playerInput)
        {
            _playerManager = playerManager;
            _playerInput = playerInput;
        }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("プレイ中ステート開始");
            _playerManager.SwitchPause(false);
            
            // PlayerInputへの購読
            _disposable = _playerInput.PauseInput
                .SkipLatestValueOnSubscribe()
                .Subscribe(toPause =>
                {
                    SwitchToPauseState = toPause;
                    if (toPause) StateChange();
                });
        }
        
        protected override void OnUpdate()
        {
            if (_playerManager.transform.position.z > _enterBossStagePosition)
            {   //プレイヤーがボスステージ開始前の位置に到達したら前進を止めて遷移
                _playerManager.SwitchPause(false);
                ArrivedBossStagePosition = true;
                StateChange();
            }
        }

        protected override void OnExit(State nextState)
        {
            _disposable.Dispose();
        }
    }
}
