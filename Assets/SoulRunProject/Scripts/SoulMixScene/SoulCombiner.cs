using System;
using System.Collections.Generic;
using System.Linq;
using SoulRun.InGame;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UniRx;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> ソウルを合成するクラス </summary>
    [Serializable]
    public class SoulCombiner : MonoBehaviour
    {
        //ownedSelectSoulsをリアクティブプロパティに修正
        public ReactiveProperty<SoulCardList>
            ownedSelectSouls = new ReactiveProperty<SoulCardList>(); // 所持しているかつ選んだソウルリスト

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

        /// <summary> 所持しているソウルカードの中から合成可能なソウルカードを探す </summary>
        public SoulCard SearchCombinableSoul(SoulCard selectedSoul)
        {
            return ownedSelectSouls.Value.soulCardList.FirstOrDefault(ownedSoul =>
                ownedSoul != selectedSoul && IsValidCombination(selectedSoul, ownedSoul));
        }

        /// <summary> 2つのソウルカードの組み合わせが有効かどうかを判定する </summary>
        private bool IsValidCombination(SoulCard soul1, SoulCard soul2)
        {
            return combinations.Any(c => c.IsValidCombination(soul1, soul2));
        }

        /// <summary> 特定のソウルカードと組み合わせ可能な組み合わせを探す共通処理 </summary>
        private SoulCombination FindCompatibleCombination(SoulCard selectedSoul)
        {
            // 組み合わせリストから選択されたソウルカードと組み合わせ可能な組み合わせを検索する。
            // 選択されたソウルカードが組み合わせの一方の成分であり、もう一方の成分が選択されたソウルカードでないことを確認する。
            return combinations.FirstOrDefault(combination =>
                (combination.Ingredient1.Equals(selectedSoul) && !combination.Ingredient2.Equals(selectedSoul)) ||
                (combination.Ingredient2.Equals(selectedSoul) && !combination.Ingredient1.Equals(selectedSoul)));
        }

        /// <summary>  選択されたソウルカードが組み合わせに使えるかどうかを判定する </summary>
        public bool IsSelectedSoul(SoulCard selectSoul)
        {
            return FindCompatibleCombination(selectSoul) != null;
        }

        /// <summary> 特定のソウルカードと組み合わせ可能なソウルカードのResultを返す </summary>
        public SoulCard SearchCombineSoul(SoulCard selectedSoul)
        {
            var combination = FindCompatibleCombination(selectedSoul);
            return combination?.Result; // null許容型のアクセス演算子を使用
        }

        /// <summary> ソウルを合成する </summary>
        public SoulCard Combine(SoulCard selectSoul1, SoulCard selectSoul2)
        {
            SoulCombination combination = combinations.Find(c => c.IsValidCombination(selectSoul1, selectSoul2));
            if (combination == null)
            {
                Debug.Log("有効な組み合わせがありません。");
                return null;
            }

            // 新しいソウルカードインスタンスを作成
            SoulCard newSoul = gameObject.AddComponent<SoulCard>();
            SetData(newSoul, combination.Result);

            ownedSelectSouls.Value.soulCardList.Remove(selectSoul1);
            ownedSelectSouls.Value.soulCardList.Remove(selectSoul2);
            // 必要に応じて新しいソウルカードをリストに追加

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