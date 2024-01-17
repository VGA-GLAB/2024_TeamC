using UnityEngine;
using UnityEngine.UI;

namespace SoulRun
{
    /// <summary>
    /// スコアを表示するクラス
    /// </summary>
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] Text _scoreText;

        public void SetScoreText(float score)
        {
            _scoreText.text = $"SCORE:{score}";
        }
    }
}
