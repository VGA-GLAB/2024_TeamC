using System;
using System.Collections.Generic;
using System.Linq;
using SoulRun.InGame;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> ソウルを合成するクラス </summary>
    public class SoulCombiner : MonoBehaviour
    {
        public SoulCardList ownedSouls; // 所持しているソウルリスト
        public List<SoulCombination> combinations; // ソウルの組み合わせリスト

        public InputUIButton soul1Button;

        private void Awake()
        {
            TryGetComponent(out soul1Button); // この使用例はもしsoul1Buttonが自動的に設定されるべきなら意味があるが、明確化が必要
            soul1Button.OnClick.AddListener(SelectSoul1);
        }

        private void SelectSoul1(InputUIButton button)
        {
            Debug.Log("SelectSoul1");
        }
        
        

        /// <summary> ソウルを合成する </summary>
        public SoulCard Combine(SoulCard selectSoul1, SoulCard selectSoul2)
        {
            if (!combinations.Any(combination =>
                    combination.IsValidCombination(selectSoul1, selectSoul2))) return null;

            ownedSouls.soulCardList.Remove(selectSoul1);
            ownedSouls.soulCardList.Remove(selectSoul2);

            // 何を作るかを決定する
            SoulCard newSoul = ScriptableObject.CreateInstance<SoulCard>();

            // 作ったソウルにDataを設定する


            // 作ったソウルをリストに追加する
            ownedSouls.soulCardList.Add(newSoul);
            return newSoul;
        }
    }
}