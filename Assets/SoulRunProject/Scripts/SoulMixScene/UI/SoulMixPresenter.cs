using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixPresenter : MonoBehaviour
    {
        [Inject] private SoulMixView _soulMixView;
        [Inject] private SoulMixModel _soulMixModel;

        private void Start()
        {
            // VContainerから依存オブジェクトが注入された後に、イベントサブスクリプションなどの初期化を行う
            _soulMixView.SoulMixButton.onClick.AsObservable().Subscribe(_ => _soulMixModel.SoulMixAsync().Forget());
            _soulMixModel.LogMessage.Subscribe(_soulMixView.DisplayLogMessage).AddTo(this);

            _soulMixModel.OwnedCards.ObserveAdd().Subscribe(ev => _soulMixView.AddCard(ev.Value)).AddTo(this);
            _soulMixModel.OwnedCards.ObserveRemove().Subscribe(_ => _soulMixView.ClearCards()).AddTo(this);
            _soulMixModel.OwnedCards.ObserveReset().Subscribe(_ => _soulMixView.ClearCards()).AddTo(this);
        }
    }
}