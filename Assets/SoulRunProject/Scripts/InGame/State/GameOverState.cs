using SoulRunProject.Common;
using SoulRunProject.Framework;

namespace SoulRunProject.InGame
{
    public class GameOverState : State
    {
        PlayerManager _playerManager;
        
        public GameOverState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ゲームオーバーステート開始");
            PauseManager.Pause(true);
        }
    }
}
