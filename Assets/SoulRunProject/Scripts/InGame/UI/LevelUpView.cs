using System;
using SoulRun.InGame;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject.InGame
{
    public class LevelUpView : MonoBehaviour
    {
        [SerializeField] private GameObject _levelUpPanel;

        /// <summary> [0]:Skill, [1,2]:Passive </summary>
        [SerializeField] private ButtonAndView[] _upgradeButtons;

        public ButtonAndView[] UpgradeButtons => _upgradeButtons;

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

        /// <summary>
        /// Buttonと表示素材をリンク
        /// </summary>
        [Serializable]
        public class ButtonAndView
        {
            [SerializeField] private InputUIButton _inputUIButton;
            [SerializeField] private Text _nameAndLevelText;
            [SerializeField] private Text _explanatoryText;
            [SerializeField] private Image _buttonIconImage;
            
            public InputUIButton InputUIButton => _inputUIButton;
            public Text NameAndLevelText => _nameAndLevelText;
            public Text ExplanatoryText => _explanatoryText;
            public Image ButtonIconImage => _buttonIconImage;
        }
    }
}
