using System;
using System.Collections.Generic;
using System.Linq;
using SoulRun.InGame;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> ソウルを合成するクラス </summary>
    [Serializable]
    public class SoulCombiner : MonoBehaviour
    {
        public SoulCardList ownedSelectSouls; // 所持しているかつ選んだソウルリスト
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

        /// <summary> 特定のソウルカードと組み合わせ可能な組み合わせを探す共通処理 </summary>
        private SoulCombination FindCompatibleCombination(SoulCard selectedSoul)
        {
            // 組み合わせリストから選択されたソウルカードと組み合わせ可能な組み合わせを検索する。
            // ただし、組み合わせの両方の成分が選択されたソウルカード自身でないことを確認する。Equalsは参照型の比較
            return combinations.FirstOrDefault(combination =>
                ((combination.Ingredient1.Equals(selectedSoul) && !combination.Ingredient2.Equals(selectedSoul)) ||
                 (combination.Ingredient2.Equals(selectedSoul) && !combination.Ingredient1.Equals(selectedSoul))));
        }

        /// <summary>  選択されたソウルカードが組み合わせに使えるかどうかを判定する </summary>
        public bool IsSelectedSoul(SoulCard selectSoul)
        {
            return FindCompatibleCombination(selectSoul) != null;
        }

        /// <summary> 特定のソウルカードと組み合わせ可能なソウルカードのリストを返す </summary>
        public SoulCard SearchCombineSoul(SoulCard selectedSoul)
        {
            var combination = FindCompatibleCombination(selectedSoul);
            return combination?.Result; // null許容型のアクセス演算子を使用
        }

        /// <summary> ソウルを合成する </summary>
        public SoulCard Combine(SoulCard selectSoul1, SoulCard selectSoul2)
        {
            ownedSelectSouls.soulCardList.Remove(selectSoul1);
            ownedSelectSouls.soulCardList.Remove(selectSoul2);

            // 何を作るかを決定する
            SoulCombination combination = combinations.Find(c => c.IsValidCombination(selectSoul1, selectSoul2));
            if (combination == null)
            {
                Debug.Log("有効な組み合わせがありません。");
                return null;
            }

            SoulCard newSoul = ScriptableObject.CreateInstance<SoulCard>();
            // 新しいソウルカードのデータを設定
            SetData(newSoul, combination.Result);


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
            newSoul.TraitList = setSoul.TraitList;
        }
    }
}