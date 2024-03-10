using SoulRun.InGame;
using UnityEngine;
using UniRx;

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixView : MonoBehaviour
    {
        [SerializeField] private InputUIButton soul1Button;
        SoulMixPresenter _presenter;

        public InputUIButton Soul1Button => soul1Button;
    }
}