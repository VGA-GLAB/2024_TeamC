using SoulRunProject.Common;
using SoulRunProject.Framework;
using UniRx;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ランゲームプレイ中の管理を行うクラス
    /// </summary>
    public class PlayingRunGameState : State
    {
        private PlayerManager _playerManager;
        private PlayerInput _playerInput;
        private PlayerLevelManager _playerLevelManager;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        //TODO：ボスステージ開始前のプレイヤーの位置を設定する場所を検討
        private float _enterBossStagePosition = 440;
        public bool ArrivedBossStagePosition { get; private set; }
        public bool SwitchToPauseState { get; private set; }
        public bool SwitchToGameOverState { get; private set; }
        public bool SwitchToLevelUpState { get; private set; }
        
        public PlayingRunGameState(PlayerManager playerManager, PlayerInput playerInput, PlayerLevelManager playerLevelManager)
        {
            _playerManager = playerManager;
            _playerInput = playerInput;
            _playerLevelManager = playerLevelManager;
        }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("プレイ中ステート開始");
            _playerManager.SwitchPause(false);
            SwitchToLevelUpState = false;
            
            // PlayerInputへの購読
            _playerInput.PauseInput
                .SkipLatestValueOnSubscribe()
                .Subscribe(toPause =>
                {
                    SwitchToPauseState = toPause;
                    if (toPause) StateChange();
                })
                .AddTo(_compositeDisposable);
            
            _playerLevelManager.OnCurrentLevelDataChanged
                .SkipLatestValueOnSubscribe()
                .Subscribe(_ =>
                {
                    SwitchToLevelUpState = true;
                    StateChange();
                })
                .AddTo(_compositeDisposable);
        }
        
        protected override void OnUpdate()
        {
            if (_playerManager.transform.position.z > _enterBossStagePosition)
            {   //プレイヤーがボスステージ開始前の位置に到達したら前進を止めて遷移
                _playerManager.SwitchPause(false);
                ArrivedBossStagePosition = true;
                StateChange();
            }
            else if (_playerManager.CurrentHp.Value <= 0)
            {   //プレイヤーのHPが0になったら遷移
                StateChange();
            }
        }

        protected override void OnExit(State nextState)
        {
            _compositeDisposable.Clear();
        }
    }
}
