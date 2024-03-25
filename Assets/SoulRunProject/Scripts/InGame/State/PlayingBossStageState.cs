using SoulRunProject.Common;
using SoulRunProject.Framework;
using UniRx;

namespace SoulRunProject.InGame
{
    public class PlayingBossStageState : State
    {
        private PlayerManager _playerManager;
        private PlayerInput _playerInput;
        private PlayerLevelManager _playerLevelManager;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public PlayingBossStageState(PlayerManager playerManager, PlayerInput playerInput, PlayerLevelManager playerLevelManager)
        {
            _playerManager = playerManager;
            _playerInput = playerInput;
            _playerLevelManager = playerLevelManager;
        }
        
        public bool IsBossDefeated { get; private set; } = false;
        public bool SwitchToPauseState { get; private set; } = false;
        public bool SwitchToLevelUpState { get; private set; } = false;
        
        protected override void OnEnter(State currentState)
        {
            DebugClass.Instance.ShowLog("ボスステージステート開始");
            //TODO：ボスステージの処理を入れる
            //ボスが倒されたらIsBossDefeatedをtrueにしてStateChangeを呼ぶ
            IsBossDefeated = true;
            StateChange();
            
            // TODO : ボスステージ処理が出来たらStateChangeではなく以降のPause、LevelUpへの登録をする
            return;
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
            // ボスステージのプレイ中の処理
            if (IsBossDefeated)
            {
                StateChange();
            }
        }

        protected override void OnExit(State nextState)
        {
            _compositeDisposable.Clear();
        }
    }
}
