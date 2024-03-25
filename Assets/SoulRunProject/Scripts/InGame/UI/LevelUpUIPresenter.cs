using UniRx;
using VContainer.Unity;

namespace SoulRunProject.InGame
{
    public class LevelUpUIPresenter : IInitializable
    {
        private readonly LevelUpState _levelUpState;
        private readonly LevelUpView _levelUpView;
        
        public LevelUpUIPresenter(LevelUpState levelUpState, LevelUpView levelUpView)
        {
            _levelUpState = levelUpState;
            _levelUpView = levelUpView;
        }

        public void Initialize()
        {
            _levelUpState.OnStateEnter += _ =>
            {
                _levelUpView.SetLevelUpPanelVisibility(true);
            };
            _levelUpState.OnStateExit += _ =>
            {
                _levelUpView.SetLevelUpPanelVisibility(false);
            };
            _levelUpView.TempOptionButton.onClick.AsObservable().Subscribe(_ =>
            {
                _levelUpState.SelectedSkill();
            });
        }
    }
}
