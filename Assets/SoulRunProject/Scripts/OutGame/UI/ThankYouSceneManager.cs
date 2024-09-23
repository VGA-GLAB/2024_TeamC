using Cysharp.Threading.Tasks;
using HikanyanLaboratory.SceneManagement;
using SoulRunProject.Audio;
using SoulRunProject.Common;
using SoulRunProject.InGame;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;

namespace SoulRunProject
{
    public class ThankYouSceneManager : MonoBehaviour
    {
        [SerializeField, CustomLabel("タイトルに遷移するまでの時間  -1の時は自動で遷移しない"), Tooltip("-1の時は遷移しない, 0以上の時はその秒数経過後に遷移する")]
        private float _duration; // -1の時は遷移しない

        [SerializeField] private string _bgmName;
        [SerializeField] private string _voiceName = "VOICE_GameEnd_1";
        [SerializeField] private Button _titleButton;
        [SerializeField] private CanvasGroup _fadePanel;

        [SerializeField, Tooltip("FadePanelの表示開始時間")]
        private float _delayTime = 3.0f;

        private SoulRunInput _input;

        private void Start()
        {
            _titleButton.onClick.AddListener(ToTitle);
            _input = new SoulRunInput();
            _input.Enable();
            ObservePlayerInput();
            CriAudioManager.Instance.Play(CriAudioType.CueSheet_BGM, _bgmName);
            CriAudioManager.Instance.Play(CriAudioType.CueSheet_VOICE, _voiceName);
            _fadePanel.alpha = 0;
            ShowNextMessage(_delayTime);
            if (_duration >= 0)
            {
                Observable.Timer(System.TimeSpan.FromSeconds(_duration))
                    .TakeUntilDestroy(this) // このコンポーネントが破棄されたらタイマーも停止
                    .Subscribe(_ => ToTitle());
            }
        }

        void ToTitle()
        {
            _ = SceneManager.Instance.LoadSceneWithFade("Title");
        }

        async void ShowNextMessage(float value)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(value));
            _fadePanel.DOFade(1, 3.0f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void ObservePlayerInput()
        {
            // Submitアクションでタイトルに戻る
            _input.UI.Submit.performed += context => ToTitle();

            // 任意のキー入力でもタイトルに戻る
            _input.Player.Menu.performed += context => ToTitle();
        }


        private void OnDestroy()
        {
            _input.Disable();
        }
    }
}