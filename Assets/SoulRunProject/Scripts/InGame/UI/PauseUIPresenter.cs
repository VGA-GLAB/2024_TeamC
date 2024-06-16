using SoulRunProject.Common;
using VContainer.Unity;
using UniRx;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ゲーム内データをView渡す
    /// </summary>
    public class PauseUIPresenter : IInitializable
    {
        private readonly PauseState _pauseState;
        private readonly PauseView _pauseView;
        private readonly PlayerManager _playerManager;
        
        public PauseUIPresenter(PauseState pauseState, PauseView pauseView, PlayerManager playerManager)
        {
            _pauseState = pauseState;
            _pauseView = pauseView;
            _playerManager = playerManager;
        }
        
        public void Initialize()
        {
            _pauseState.OnStateEnter += _ =>
            {
                _pauseView.SetDisplay(true);
                _pauseView.ReflectGameData(ScoreManager.Instance.OnScoreChanged.Value, _playerManager.ResourceContainer.Coin);
            };

            _pauseState.OnStateExit += _ =>
            {
                _pauseView.SetDisplay(false);
            };
            
            _pauseView.ExitButton.onClick.AsObservable().Subscribe(_ =>
            {
                _pauseState.ExitToTitle();
            }).AddTo(_pauseView);
        }
    }
}