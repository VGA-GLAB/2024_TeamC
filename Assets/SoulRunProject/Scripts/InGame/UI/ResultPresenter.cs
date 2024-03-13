using SoulRunProject.Framework;
using VContainer.Unity;
using UniRx;

namespace SoulRunProject.InGame
{
    public class ResultPresenter : IInitializable
    {
        ResultView _resultView;
        GameClearState _gameClearState;
        GameOverState _gameOverState;
        
        public ResultPresenter(ResultView resultView, GameClearState gameClearState, GameOverState gameOverState)
        {
            _resultView = resultView;
            _gameClearState = gameClearState;
            _gameOverState = gameOverState;
        }

        public void Initialize()
        {
            _gameClearState.OnStateEnter += _ =>
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
                _gameClearState.RetryStage();
            });
            _resultView.ExitButton.onClick.AsObservable().Subscribe(_ =>
            {
                _resultView.SetResultPanelVisibility(false);
                _gameClearState.ExitToTitle();
            });
            
            _resultView.SetResultPanelVisibility(false);
        }
    }
}
