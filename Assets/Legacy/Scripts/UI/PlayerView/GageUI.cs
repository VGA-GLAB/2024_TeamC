using UnityEngine;
using UnityEngine.UI;

namespace SoulRun.InGame
{
    /// <summary>
    /// ゲージのUI処理を行う
    /// </summary>
    public class GageUI : MonoBehaviour
    {
        [SerializeField] Image gageUI;
        private float _maxFillAmount = 1.0f;

        /// <summary>
        /// ゲージの最大値と現在のゲージの値を設定する
        /// </summary>
        /// <param name="max"></param>
        /// <param name="start"></param>
        public void SetGageUI(float max, float start)
        {
            _maxFillAmount = max;
            SetCurrentGage(start);
        }

        /// <summary>
        /// 現在のゲージの値を設定する
        /// </summary>
        public void SetCurrentGage(float current)
        {
            float currentGageRatio = current / _maxFillAmount;
            gageUI.fillAmount = currentGageRatio;
        }

        public void SetMax(float max)
        {
            _maxFillAmount = max;
        }
    }
}
