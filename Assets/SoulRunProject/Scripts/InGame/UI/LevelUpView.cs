using System;
using SoulRun.InGame;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class LevelUpView : MonoBehaviour
    {
        [SerializeField] private GameObject _levelUpPanel;
        [SerializeField] private InputUIButton _tempOptionButton;

        public InputUIButton TempOptionButton => _tempOptionButton;

        private void Start()
        {
            SetLevelUpPanelVisibility(false);
        }

        /// <summary>
        /// LevelUpPanelの表示を切り替える
        /// </summary>
        /// <param name="isShow"></param>
        public void SetLevelUpPanelVisibility(bool isShow)
        {
            _levelUpPanel.SetActive(isShow);
        }
    }
}
