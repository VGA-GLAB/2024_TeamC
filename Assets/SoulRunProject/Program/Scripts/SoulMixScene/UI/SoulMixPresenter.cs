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
            _soulMixView.Soul1Button.onClick.AsObservable().Subscribe(_ => _soulMixModel.SoulMixAsync().Forget());
        }
    }
}