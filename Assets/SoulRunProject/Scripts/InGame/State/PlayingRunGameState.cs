using SoulRunProject.Common;
using SoulRunProject.Framework;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ランゲームプレイ中の管理を行うクラス
    /// </summary>
    public class PlayingRunGameState : State
    {
        private PlayerManager _playerManager;
        
        //TODO：ボスステージ開始前のプレイヤーの位置を設定する場所を検討
        private float _enterBossStagePosition = 1000f;
        public bool ArrivedBossStagePosition { get; private set; } = false;
        
        public PlayingRunGameState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("プレイ中ステート開始");
            _playerManager.SwitchPause(false);
        }
        
        protected override void OnUpdate()
        {
            if (_playerManager.transform.position.z > _enterBossStagePosition)
            {   //プレイヤーがボスステージ開始前の位置に到達したら前進を止めて遷移
                _playerManager.SetPlayerForwardMover(false);
                ArrivedBossStagePosition = true;
                StateChange();
            }
        }
        
    }
}
