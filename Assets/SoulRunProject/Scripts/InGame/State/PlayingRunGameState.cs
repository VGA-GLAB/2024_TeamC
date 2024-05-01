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
        private StageManager _stageManager;
        private PlayerManager _playerManager;
        private PlayerInput _playerInput;
        private PlayerLevelManager _playerLevelManager;
        private FieldMover _fieldMover;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public bool ArrivedBossStagePosition { get; private set; }
        public bool SwitchToPauseState { get; private set; }
        public bool IsPlayerDead { get; private set; }
        public bool SwitchToLevelUpState { get; private set; }
        public PlayingRunGameState(StageManager stageManager, PlayerManager playerManager, 
            PlayerInput playerInput, PlayerLevelManager playerLevelManager)
        {
            _stageManager = stageManager;
            _playerManager = playerManager;
            _playerInput = playerInput;
            _playerLevelManager = playerLevelManager;
            _fieldMover = fieldMover;
        }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("プレイ中ステート開始");
            PauseManager.Pause(false);
            SwitchToLevelUpState = false;
            ArrivedBossStagePosition = false;
            
            // PlayerInputへの購読
            _playerInput.PauseInput
                .SkipLatestValueOnSubscribe()
                .Subscribe(toPause =>
                {
                    SwitchToPauseState = toPause;
                    if (toPause) StateChange();
                })
                .AddTo(_compositeDisposable);
            
            // レベルアップの購読
            _playerLevelManager.OnLevelUp
                .SkipLatestValueOnSubscribe()
                .Subscribe(_ =>
                {
                    SwitchToLevelUpState = true;
                    StateChange();
                })
                .AddTo(_compositeDisposable);
            //_fieldMover.StartBossStage += StartBossStage;
        }
        
        protected override void OnUpdate()
        { 
            if (_playerManager.CurrentHp.Value <= 0)
            {   //プレイヤーのHPが0になったら遷移
                IsPlayerDead = true;
                StateChange();
            }
            // プレイヤーのHPの監視
            _playerManager.CurrentHp
                .Where(hp => hp <= 0)
                .Subscribe(hp =>
                {
                    IsPlayerDead = true;
                    StateChange();
                })
                .AddTo(_compositeDisposable);
            
            // ボスステージへの遷移への購読
            _stageManager.ToBossStage += () =>
            {
                ArrivedBossStagePosition = true;
                StateChange();
            };
            _stageManager.PlayingRunGameUpdate();
        }

        protected override void OnExit(State nextState)
        {
            _compositeDisposable.Clear();
            _fieldMover.StartBossStage -= StartBossStage;
            ArrivedBossStagePosition = false;
        }

        /// <summary>
        /// ボスステージ開始
        /// </summary>
        void StartBossStage()
        {
            PauseManager.Pause(false);
            ArrivedBossStagePosition = true;
            StateChange();
        }
    }
}
