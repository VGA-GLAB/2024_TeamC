using SoulRun.InGame;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// PauseUIの変更処理
    /// </summary>
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _coinText;
        [SerializeField] private InputUIButton _resumeButton;
        [SerializeField] private InputUIButton _exitButton;

        public InputUIButton ResumeButton => _resumeButton;
        public InputUIButton ExitButton => _exitButton;

        private void Start()
        {
            SetDisplay(false);
        }

        /// <summary> UIの表示を切り替える </summary>
        /// <param name="display"></param>
        public void SetDisplay(bool display)
        {
            _pausePanel.SetActive(display);
        }

        /// <summary> ゲーム内データをUIへ反映させる </summary>
        public void ReflectGameData(int score, int coin)
        {
            _scoreText.text = score.ToString();
            _coinText.text = coin.ToString();
        }
    }
}