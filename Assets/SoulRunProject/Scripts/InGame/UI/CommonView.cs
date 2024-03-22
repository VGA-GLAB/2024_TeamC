using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// インゲームで常時表示されるUIを管理するクラス
    /// ・体力、経験値、レベル、所持スキル、スコア、コイン、ソウルゲージの操作をする
    /// </summary>
    public class CommonView : MonoBehaviour
    {
        [SerializeField] private Image _hpGauge;
        [SerializeField] private Image _expGauge;
        [SerializeField] private Image _soulGauge;
        [SerializeField] private Text _levelText;
        [SerializeField] private List<Image> _skillIcons;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _coinText;
        
        public void SetHpGauge(float value, float maxValue)
        {
            _hpGauge.fillAmount = value / maxValue;
        }
        
        public void SetExpGauge(float value, float maxValue)
        {
            _expGauge.fillAmount = value / maxValue;
        }
        
        public void SetSoulGauge(float value, float maxValue)
        {
            _soulGauge.fillAmount = value / maxValue;
        }
        
        public void SetLevelText(int level)
        {
            _levelText.text = $"{level}";
        }
        
        public void SetSkillIcon(int index, Sprite sprite)
        {
            _skillIcons[index].sprite = sprite;
        }
        
        public void SetScoreText(int score)
        {
            _scoreText.text = $"SCORE:{score}";
        }
        
        public void SetCoinText(int coin)
        {
            _coinText.text = $"{coin}";
        }
    }
}
