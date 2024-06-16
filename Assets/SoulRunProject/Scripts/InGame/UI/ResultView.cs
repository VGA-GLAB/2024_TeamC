using System;
using System.Collections;
using System.Collections.Generic;
using SoulRun.InGame;
using SoulRunProject.Framework;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// リザルトのUI関連の処理を行うクラス
    /// </summary>
    public class ResultView : MonoBehaviour
    {
        [SerializeField] private InputUIButton _restartButton;
        [SerializeField] private InputUIButton _exitButton;
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _coinText;
        
        public InputUIButton RestartButton => _restartButton;
        public InputUIButton ExitButton => _exitButton;

        private void Start()
        {
            _restartButton.OnClickAsObservable().Subscribe(_ => DebugClass.Instance.ShowLog("リスタートボタンが押されました。"));
        }

        /// <summary>
        /// リザルト画面の表示非表示を設定する
        /// </summary>
        /// <param name="isShow"></param>
        public void SetResultPanelVisibility(bool isShow)
        {
            _resultPanel.SetActive(isShow);
        }
        
        public void ShowResult(int score, int coin)
        {
            _scoreText.text = score.ToString();
            _coinText.text = coin.ToString();
        }
        
        public enum ResultType
        {
            Clear,
            GameOver,
        }
    }
}
