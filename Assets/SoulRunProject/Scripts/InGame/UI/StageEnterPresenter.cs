using UnityEngine;
using VContainer.Unity;

namespace SoulRunProject.InGame
{
    public class StageEnterPresenter : IStartable
    {
        [SerializeField] private StageNameView _stageNameView;
        [SerializeField] private EnterStageState _enterStageState;
        
        public StageEnterPresenter(StageNameView stageNameView, EnterStageState enterStageState)
        {
            _stageNameView = stageNameView;
            _enterStageState = enterStageState;
        }

        public void Start()
        {
            _enterStageState.OnStateEnter += _ =>
            {
                _stageNameView.ShowStageName();
            };
        }
    }
}
