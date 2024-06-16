using SoulRunProject.Common;
using SoulRunProject.Framework;
using VContainer.Unity;
using UniRx;

namespace SoulRunProject.InGame
{
    public class ResultPresenter : IInitializable
    {
        ResultView _resultView;
        ResultState _resultState;
        private PlayerManager _playerManager;
        
        public ResultPresenter(ResultView resultView, ResultState resultState, PlayerManager playerManager)
        {
            _resultView = resultView;
            _resultState = resultState;
            _playerManager = playerManager;
        }

        public void Initialize()
        {
            _resultState.OnStateEnter += _ =>
            {
                _resultView.SetResultPanelVisibility(true);
                _resultView.ShowResult(ScoreManager.Instance.OnScoreChanged.Value, _playerManager.ResourceContainer.Coin);
            };
            _resultView.RestartButton.onClick.AsObservable().Subscribe(_ =>
            {
                DebugClass.Instance.ShowLog("リスタートボタンが押されました。");
                _resultView.SetResultPanelVisibility(false);
                _resultState.RetryStage();
            });
            _resultView.ExitButton.onClick.AsObservable().Subscribe(_ =>
            {
                _resultView.SetResultPanelVisibility(false);
                _resultState.ExitToTitle();
            });
            
            _resultView.SetResultPanelVisibility(false);
        }
    }
}
