using UnityEngine;

namespace SoulRunProject.Common
{
    public static class Calculator
    {
        /// <param name="baseDamage">計算前ダメージ</param>
        /// <param name="defence">防御力</param>
        /// <param name="criticalRate">クリティカル率</param>
        /// <param name="cirticalDamageRate">クリティカルダメージ倍率</param>
        /// <returns>計算後ダメージ</returns>
        public static float CalcDamage(float baseDamage, int defence, float criticalRate, float cirticalDamageRate)
        {
            return baseDamage * 100 / (100 + defence) * Random.Range(0, 100f) < criticalRate ? cirticalDamageRate : 1f;
        }
        /// <param name="baseDamage">計算前ダメージ</param>
        /// <param name="defence">防御力</param>
        /// <param name="criticalRate">クリティカル率</param>
        /// <param name="cirticalDamageRate">クリティカルダメージ倍率</param>
        /// <returns>計算後ダメージ</returns>
        public static float CalcDamage(float baseDamage, int defence, float criticalRate, float cirticalDamageRate, ref bool isCritical)
        {
            float multiply = 1f;
            if (Random.Range(0, 100f) < criticalRate)
            {
                multiply = cirticalDamageRate;
                isCritical = true;
            }
            return baseDamage * 100 / (100 + defence) * multiply;
        }
    }
}
