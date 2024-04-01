using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Cysharp.Threading.Tasks; // UniTaskを使用するため

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixPresenter : IStartable
    {
        private readonly SoulMixView _soulMixView;
        private readonly SoulMixModel _soulMixModel;

        [Inject]
        public SoulMixPresenter(SoulMixModel model, SoulMixView view)
        {
            _soulMixModel = model;
            _soulMixView = view;
        }

        public void Start()
        {
            // イベントサブスクリプションなどの初期化を行う
            _soulMixView.SoulMixButton.onClick.AsObservable().Subscribe(_ =>
                _soulMixModel.SoulMixAsync().Forget());
            _soulMixModel.LogMessage.Subscribe(_soulMixView.DisplayLogMessage).AddTo(_soulMixView);

            _soulMixModel.OwnedCards.ObserveAdd().Subscribe(ev =>
                _soulMixView.AddCard(ev.Value)).AddTo(_soulMixView);
            _soulMixModel.OwnedCards.ObserveRemove().Subscribe(_ =>
                _soulMixView.ClearCards()).AddTo(_soulMixView);
            _soulMixModel.OwnedCards.ObserveReset().Subscribe(_ =>
                _soulMixView.ClearCards()).AddTo(_soulMixView);
        }
    }
}