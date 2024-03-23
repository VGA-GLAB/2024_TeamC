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
        public SoulCardData SelectSoul()
        {
            // クリックされたソウルを取得する
            SoulCardData selectSoul = null;
            // クリックされたソウルを取得する
            if (EventSystem.current.currentSelectedGameObject.TryGetComponent(out SoulCardData soulCard))
            {
                selectSoul = soulCard;
            }

            return selectSoul;
        }
        /// <summary> 所持しているソウルカードの中から合成可能なソウルカードを探す </summary>
        public SoulCardData SearchCombinableSoul(SoulCardData selectedSoul)
        {
            return ownedSelectSouls.soulCardList.FirstOrDefault(ownedSoul => 
                    ownedSoul != selectedSoul && IsValidCombination(selectedSoul, ownedSoul));
        }
        
        /// <summary> 2つのソウルカードの組み合わせが有効かどうかを判定する </summary>
        private bool IsValidCombination(SoulCardData soul1, SoulCardData soul2)
        {
            return combinations.Any(c => c.IsValidCombination(soul1, soul2));
        }
        
        /// <summary> 特定のソウルカードと組み合わせ可能な組み合わせを探す共通処理 </summary>
        private SoulCombination FindCompatibleCombination(SoulCardData selectedSoul)
        {
            // 組み合わせリストから選択されたソウルカードと組み合わせ可能な組み合わせを検索する。
            // 選択されたソウルカードが組み合わせの一方の成分であり、もう一方の成分が選択されたソウルカードでないことを確認する。
            return combinations.FirstOrDefault(combination =>
                (combination.Ingredient1.Equals(selectedSoul) && !combination.Ingredient2.Equals(selectedSoul)) ||
                (combination.Ingredient2.Equals(selectedSoul) && !combination.Ingredient1.Equals(selectedSoul)));
            
            
        }

        /// <summary>  選択されたソウルカードが組み合わせに使えるかどうかを判定する </summary>
        public bool IsSelectedSoul(SoulCardData selectSoul)
        {
            return FindCompatibleCombination(selectSoul) != null;
        }

        /// <summary> 特定のソウルカードと組み合わせ可能なソウルカードのResultを返す </summary>
        public SoulCardData SearchCombineSoul(SoulCardData selectedSoul)
        {
            var combination = FindCompatibleCombination(selectedSoul);
            return combination?.Result; // null許容型のアクセス演算子を使用
        }

        /// <summary> ソウルを合成する </summary>
        public SoulCardData Combine(SoulCardData selectSoul1, SoulCardData selectSoul2)
        {
            // 何を作るかを決定する
            SoulCombination combination = combinations.Find(c => 
                c.IsValidCombination(selectSoul1, selectSoul2));
            if (combination == null)
            {
                Debug.Log("有効な組み合わせがありません。");
                return null;
            }

            SoulCardData newSoul = ScriptableObject.CreateInstance<SoulCardData>();
            // 新しいソウルカードのデータを設定
            SetData(newSoul, combination.Result);

            ownedSelectSouls.soulCardList.Remove(selectSoul1);
            ownedSelectSouls.soulCardList.Remove(selectSoul2);

            return newSoul;
        }


        // Dataを設定する
        public void SetData(SoulCardData newSoul, SoulCardData setSoul)
        {
            newSoul.SoulName = setSoul.SoulName;
            newSoul.SoulLevel = setSoul.SoulLevel;
            newSoul.SoulAbility = setSoul.SoulAbility;
            newSoul.Status = setSoul.Status;
            newSoul.TraitList = setSoul.TraitList;
        }
    }
}