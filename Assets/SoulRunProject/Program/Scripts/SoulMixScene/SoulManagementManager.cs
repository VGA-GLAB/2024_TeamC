using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    public class SoulManagementManager : MonoBehaviour
    {
        // 新しいソウルを作成するメソッド（配合）
        public void CombineSoulCards()
        {
            
        }

        // ソウルカードのレベルアップメソッド
        public void LevelUpSoulCard(SoulCard soulCard)
        {
            if (soulCard.SoulLevel < 10) // 仮に最大レベルを10とする
            {
                soulCard.SoulLevel += 1;
            }
        }
    }
}