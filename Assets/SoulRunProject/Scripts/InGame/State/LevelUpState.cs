using SoulRunProject.Common;

namespace SoulRunProject.InGame
{
    public class LevelUpState : State
    {
        private PlayerManager _playerManager;
        private State _lastState;

        public State StateToReturn => _lastState;
        
        public LevelUpState(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        protected override void OnEnter(State currentState)
        {
            _lastState = currentState;
            _playerManager.SwitchPause(true);
        }

        public void SelectedSkill()
        {
            _playerManager.SwitchPause(false);
            StateChange();
        }
    }
}
