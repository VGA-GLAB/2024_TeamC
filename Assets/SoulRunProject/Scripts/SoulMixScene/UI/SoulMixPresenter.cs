using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixPresenter : MonoBehaviour
    {
        [SerializeField] private SoulMixView _soulMixView;
        [SerializeField] private SoulMixModel _soulMixModel;

        private void Start()
        {
            _soulMixView.SoulMixButton.onClick.AsObservable().Subscribe(_ => _soulMixModel.SoulMixAsync().Forget());
            _soulMixModel.LogMessage.Subscribe(_soulMixView.DisplayLogMessage).AddTo(this);

            _soulMixModel.OwnedCards.ObserveAdd().Subscribe(ev => _soulMixView.AddCard(ev.Value)).AddTo(this);
            _soulMixModel.OwnedCards.ObserveRemove().Subscribe(_ => _soulMixView.ClearCards()).AddTo(this);
            _soulMixModel.OwnedCards.ObserveReset().Subscribe(_ => _soulMixView.ClearCards()).AddTo(this);
        }
    }
}