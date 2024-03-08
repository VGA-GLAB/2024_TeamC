using System.Collections;
using System.Collections.Generic;
using SoulRun.InGame;
using UnityEngine;

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
        
        /// <summary>
        /// リザルト画面の表示非表示を設定する
        /// </summary>
        /// <param name="isShow"></param>
        public void SetResultPanelvisibility(bool isShow)
        {
            _resultPanel.SetActive(isShow);
        }
    }
}
