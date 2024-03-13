using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤーの状態をUIに反映
    /// </summary>
    public class PlayerStatusView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Slider _expSlider;
        
        private void Awake()
        {
            PlayerLevelManager pLevelManager = GetComponent<PlayerLevelManager>();
            
            // テキスト更新のSubscribe
            pLevelManager.OnCurrentLevelDataChanged.Subscribe(levelData =>
            {
                _levelText.text = levelData.CurrentLevel.ToString();
                _expSlider.maxValue = levelData.ExpToNextLevel;
            });
            pLevelManager.OnCurrentExpChanged.Subscribe(exp => _expSlider.value = exp);
        }
    }
}
