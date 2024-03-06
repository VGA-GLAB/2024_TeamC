using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRunProject.InGame;
using UniRx;
using UniRx.Triggers;

namespace SoulRunProject.Common
{
    /// <summary>
    /// ランゲームプレイ中の管理を行うクラス
    /// </summary>
    public class PlayingRunGameState : State
    {
        private PlayerMovement _playerMovement;
        private PlayerForwardMover _playerForwardMover;
        
        //TODO：ボスステージ開始前のプレイヤーの位置を設定する場所を検討
        private float _enterBossStagePosition = 1000f;
        public bool ArrivedBossStagePosition { get; private set; } = false;
        
        public PlayingRunGameState(PlayerMovement playerMovement,
            PlayerForwardMover playerForwardMover,
            EnterBossStageState enterBossStageState)
        {
            _playerMovement = playerMovement;
            _playerForwardMover = playerForwardMover;
        }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("プレイ中ステート開始");
            _playerMovement.enabled = true;
            _playerForwardMover.IsActivate(true);
        }
        
        protected override void OnUpdate()
        {
            DebugClass.Instance.ShowLog(_playerMovement.transform.position.z.ToString());
            if (_playerMovement.transform.position.z > _enterBossStagePosition)
            {   //プレイヤーがボスステージ開始前の位置に到達したら前進を止めて遷移
                _playerForwardMover.IsActivate(false);
                ArrivedBossStagePosition = true;
                StateChange();
            }
        }
        
    }
}
