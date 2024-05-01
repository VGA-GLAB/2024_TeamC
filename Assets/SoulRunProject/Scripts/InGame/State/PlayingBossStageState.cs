using SoulRunProject.Common;
using SoulRunProject.Framework;
using UniRx;

namespace SoulRunProject.InGame
{
    public class PlayingBossStageState : State
    {
        private StageManager _stageManager;
        private PlayerManager _playerManager;
        private PlayerInput _playerInput;
        private PlayerLevelManager _playerLevelManager;
        private FieldMover _fieldMover;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        public PlayingBossStageState(StageManager stageManager, PlayerManager playerManager, 
            PlayerInput playerInput, PlayerLevelManager playerLevelManager)
        {
            _stageManager = stageManager;
            _playerManager = playerManager;
            _playerInput = playerInput;
            _playerLevelManager = playerLevelManager;
            _fieldMover = fieldMover;
        }
        
        public bool IsBossDefeated { get; private set; }
        public bool SwitchToPauseState { get; private set; }
        public bool SwitchToLevelUpState { get; private set; }
        public bool IsPlayerDead { get; private set; }
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ボスステージステート開始");
            SwitchToLevelUpState = false;
            IsPlayerDead = false;
            
            _playerInput.PauseInput
                .SkipLatestValueOnSubscribe()
                .Subscribe(toPause =>
                {
                    SwitchToPauseState = toPause;
                    if (toPause) StateChange();
                })
                .AddTo(_compositeDisposable);
            
            _playerLevelManager.OnLevelUp
                .SkipLatestValueOnSubscribe()
                .Subscribe(_ =>
                {
                    SwitchToLevelUpState = true;
                    StateChange();
                })
                .AddTo(_compositeDisposable);
            
            // プレイヤーのHPの監視
            _playerManager.CurrentHp
                .Where(hp => hp <= 0)
                .Subscribe(hp =>
                {
                    IsPlayerDead = true;
                    StateChange();
                })
                .AddTo(_compositeDisposable);

            _stageManager.ToNextStage += () =>
            {
                IsBossDefeated = true;
                StateChange();
            };
        }
        
        protected override void OnUpdate()
        {
            
        }

        protected override void OnExit(State nextState)
        {
            _compositeDisposable.Clear();
            _fieldMover.NextField();
        }
    }
}
