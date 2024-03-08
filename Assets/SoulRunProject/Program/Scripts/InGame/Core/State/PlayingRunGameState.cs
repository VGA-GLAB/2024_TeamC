using SoulRunProject.Framework;
using SoulRunProject.InGame;

namespace SoulRunProject.Common
{
    /// <summary>
    /// ランゲームプレイ中の管理を行うクラス
    /// </summary>
    public class PlayingRunGameState : State
    {
        private PlayerMovement _playerMovement;
        private PlayerForwardMover _playerForwardMover;
        public PlayingRunGameState(PlayerMovement playerMovement, PlayerForwardMover playerForwardMover)
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
        
    }
}
