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


        /// <summary> ソウルを選択する </summary>
        public SoulCard SelectSoul()
        {
            // クリックされたソウルを取得する
            SoulCard selectSoul = null;
            // クリックされたソウルを取得する
            if (EventSystem.current.currentSelectedGameObject.TryGetComponent(out SoulCard soulCard))
            {
                selectSoul = soulCard;
            }

            return selectSoul;
        }

        /// <summary> 特定のソウルカードと組み合わせ可能なソウルカードのリストを返す </summary>
        public List<SoulCard> SearchCombineSoul(SoulCard selectedSoul)
        {
            return ownedSouls.soulCardList.Where(soul =>
                    combinations.Any(combination =>
                        combination.IsValidCombination(selectedSoul, soul)))
                .ToList();
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

        // Dataを設定する
        public void SetData(SoulCard newSoul, SoulCard setSoul)
        {
            newSoul.SoulName = setSoul.SoulName;
            newSoul.SoulLevel = setSoul.SoulLevel;
            newSoul.SoulAbility = setSoul.SoulAbility;
            newSoul.ExplanatoryText = setSoul.ExplanatoryText;
            newSoul.Status = setSoul.Status;
        }
    }
}