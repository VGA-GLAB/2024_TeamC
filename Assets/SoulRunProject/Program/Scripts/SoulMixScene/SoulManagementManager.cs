using System.Collections.Generic;
using System.Linq;
using SoulRun.InGame;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SoulRunProject.SoulMixScene
{
    public class SoulManagementManager : MonoBehaviour
    {
        public InputUIButton soul1Button;

        private void Awake()
        {
            TryGetComponent(out soul1Button);
            soul1Button.onClick.AddListener(SelectSoul1);
        }

        public void SelectSoul1(InputUIButton button)
        {
            Debug.Log("SelectSoul1");
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