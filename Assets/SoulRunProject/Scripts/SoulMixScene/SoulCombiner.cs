using System;
using System.Collections.Generic;
using System.Linq;
using SoulRun.InGame;
using SoulRunProject.SoulRunProject.Scripts.Common.Core.Singleton;
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
        private List<SoulCardMasterData> ownedSelectSouls;
        public List<SoulCombination> combinations; // ソウルの組み合わせリスト

        private void Start()
        {
            if (MyRepository.Instance.TryGetDataList<SoulCardMasterData>(out var dataList))
            {
                ownedSelectSouls = dataList;
            }
        }

        /// <summary> ソウルを選択する </summary>
        public SoulCardMasterData SelectSoul()
        {
            // クリックされたソウルを取得する
            SoulCardMasterData selectSoul = null;
            // クリックされたソウルを取得する
            if (EventSystem.current.currentSelectedGameObject.TryGetComponent(out SoulCardMasterData soulCard))
            {
                selectSoul = soulCard;
            }

            return selectSoul;
        }
        /// <summary> 所持しているソウルカードの中から合成可能なソウルカードを探す </summary>
        public SoulCardMasterData SearchCombinableSoul(SoulCardMasterData selectedSoul)
        {
            return ownedSelectSouls.FirstOrDefault(ownedSoul => 
                    ownedSoul != selectedSoul && IsValidCombination(selectedSoul, ownedSoul));
        }
        
        /// <summary> 2つのソウルカードの組み合わせが有効かどうかを判定する </summary>
        private bool IsValidCombination(SoulCardMasterData soul1, SoulCardMasterData soul2)
        {
            return combinations.Any(c => c.IsValidCombination(soul1, soul2));
        }
        
        /// <summary> 特定のソウルカードと組み合わせ可能な組み合わせを探す共通処理 </summary>
        private SoulCombination FindCompatibleCombination(SoulCardMasterData selectedSoul)
        {
            // 組み合わせリストから選択されたソウルカードと組み合わせ可能な組み合わせを検索する。
            // 選択されたソウルカードが組み合わせの一方の成分であり、もう一方の成分が選択されたソウルカードでないことを確認する。
            return combinations.FirstOrDefault(combination =>
                (combination.Ingredient1.Equals(selectedSoul) && !combination.Ingredient2.Equals(selectedSoul)) ||
                (combination.Ingredient2.Equals(selectedSoul) && !combination.Ingredient1.Equals(selectedSoul)));
            
            
        }

        /// <summary>  選択されたソウルカードが組み合わせに使えるかどうかを判定する </summary>
        public bool IsSelectedSoul(SoulCardMasterData selectSoul)
        {
            return FindCompatibleCombination(selectSoul) != null;
        }

        /// <summary> 特定のソウルカードと組み合わせ可能なソウルカードのResultを返す </summary>
        public SoulCardMasterData SearchCombineSoul(SoulCardMasterData selectedSoul)
        {
            var combination = FindCompatibleCombination(selectedSoul);
            return combination?.Result; // null許容型のアクセス演算子を使用
        }

        /// <summary> ソウルを合成する </summary>
        public SoulCardMasterData Combine(SoulCardMasterData selectSoul1, SoulCardMasterData selectSoul2)
        {
            // 何を作るかを決定する
            SoulCombination combination = combinations.Find(c => 
                c.IsValidCombination(selectSoul1, selectSoul2));
            if (combination == null)
            {
                Debug.Log("有効な組み合わせがありません。");
                return null;
            }

            SoulCardMasterData newSoul = ScriptableObject.CreateInstance<SoulCardMasterData>();
            // 新しいソウルカードのデータを設定
            //SetData(newSoul, combination.Result);

            // ownedSelectSouls.SoulCardMasterDataList.Remove(selectSoul1);
            // ownedSelectSouls.SoulCardMasterDataList.Remove(selectSoul2);

            return newSoul;
        }


        // Dataを設定する
        // public void SetData(SoulCardMasterData newSoul, SoulCardMasterData setSoul)
        // {
        //     newSoul.SoulName = setSoul.SoulName;
        //     newSoul.SoulLevel = setSoul.SoulLevel;
        //     newSoul.SoulAbility = setSoul.SoulAbility;
        //     newSoul.Status = setSoul.Status;
        //     newSoul.TraitList = setSoul.TraitList;
        // }
    }
}