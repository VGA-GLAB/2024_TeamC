using SoulRunProject.Framework;
using VContainer.Unity;
using UniRx;

namespace SoulRunProject.InGame
{
    public class ResultPresenter : IInitializable
    {
        ResultView _resultView;
        ResultState _resultState;
        GameOverState _gameOverState;
        
        public ResultPresenter(ResultView resultView, ResultState resultState, GameOverState gameOverState)
        {
            _resultView = resultView;
            _resultState = resultState;
            _gameOverState = gameOverState;
        }

        public void Initialize()
        {
            _resultState.OnStateEnter += _ =>
            {
                _resultView.SetResultPanelVisibility(true);
                _resultView.ShowResult(ResultView.ResultType.Clear);
            };
            _gameOverState.OnStateEnter += _ =>
            {
                _resultView.SetResultPanelVisibility(true);
                _resultView.ShowResult(ResultView.ResultType.GameOver);
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
