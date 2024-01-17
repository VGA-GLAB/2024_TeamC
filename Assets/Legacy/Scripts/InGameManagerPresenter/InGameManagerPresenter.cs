using VContainer;
using VContainer.Unity;

namespace SoulRun.InGame
{
    public class InGameManagerPresenter : IStartable
    {
        InGameManager _inGameManager;
        GachaView _gachaView;

        [Inject]
        public InGameManagerPresenter(InGameManager inGameManager, GachaView gachaView)
        {
            _inGameManager = inGameManager;
            _gachaView = gachaView;
        }

        public void Start()
        {
            BindMethod();
        }

        private void BindMethod()
        {
            _inGameManager.OnGacha += () => _gachaView.SelectReward();
        }
    }
}
