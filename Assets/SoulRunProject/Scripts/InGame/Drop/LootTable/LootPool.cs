// using System;
// using System.Collections.Generic;
// using System.Linq;
// using SoulRunProject.SoulMixScene;
// using UnityEngine;
// using Random = UnityEngine.Random;
//
// namespace SoulRunProject.Common
// {
//     /// <summary>
//     /// ロールの回数分アイテムをランダムで選んで出力するためのクラス。
//     /// </summary>
//     [Serializable]
//     public class LootPool
//     {
//         [SerializeReference, SubclassSelector] 
//         ICount _rolls;
//         [SerializeField, Tooltip("ロール数に加算するLuck値の乗数")] 
//         float _bonusRolls = 0;
//         [SerializeField] 
//         List<LootEntry> _entries;
//
//         int GetRolls(Status status = null)
//         {
//             if (_rolls == null) return 0;
//             if (status == null)
//             {
//                 return _rolls.GetCount;
//             }
//             return (int)(_rolls.GetCount + _bonusRolls * status.Luck);
//         }
//
//         public List<DropData> Choose(Status status = null)
//         {
//             var weights = _entries.Select(e => e.GetWeight(status)).ToList();
//             List<DropData> dropData = new();
//             for (int i = 0; i < GetRolls(status); i++)
//             {
//                 var chooseEntry = _entries[WeightedChooseIndex(weights)];
//                 dropData.Add(new DropData(chooseEntry.Drop, chooseEntry.GetCount(status)));
//             }
//
//             return dropData;
//         }
//
//         int WeightedChooseIndex(List<int> weights)
//         {
//             var totalWeight = weights.Sum();
//             var randomPoint = Random.Range(0, totalWeight);
//             var currentWeight = 0;
//                     
//             for (var i = 0; i < weights.Count; i++)
//             {
//                 // 現在要素までの重みの総和を求める
//                 currentWeight += weights[i];
//
//                 // 乱数値が現在要素の範囲内かチェック
//                 if (randomPoint < currentWeight)
//                 {
//                     return i;
//                 }
//             }
//             // 乱数値が重みの総和以上なら末尾要素とする
//             return weights.Count - 1;
//         }
//     }
// }