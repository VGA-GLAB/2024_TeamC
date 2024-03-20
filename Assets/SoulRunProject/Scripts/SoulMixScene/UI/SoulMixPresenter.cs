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
            // ボタンの購読設定はそのまま
            _soulMixView.SoulMix.onClick.AsObservable().Subscribe(_ => _soulMixModel.SoulMixAsync().Forget());

            // SoulCombiner の ownedSelectSouls の変更を監視し、Viewを更新する
            _soulMixModel.SoulCombiner.ownedSelectSouls.Subscribe(_ => 
                    _soulMixView.SetupReactiveUI(_soulMixModel.SoulCombiner.ownedSelectSouls, _soulMixModel.OwnerCardList.soulCardList))
                .AddTo(this);

            // 初期状態でUIをセットアップ
            _soulMixView.SetupReactiveUI(_soulMixModel.SoulCombiner.ownedSelectSouls, _soulMixModel.OwnerCardList.soulCardList);
        }
    }
}