using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixModel : MonoBehaviour
    {
        [SerializeField] private SoulCombiner _soulCombiner;
        [SerializeField] private SoulCardList _ownerCardList;

        public async UniTaskVoid SoulMixAsync()
        {
            // 選択されたソウルカードのリストが2つ以上であるか確認
            if (_soulCombiner.ownedSelectSouls.soulCardList.Count < 2)
            {
                Debug.Log("ソウルカードを2つ以上選択してください。");
                return;
            }

            // 選択された最初のソウルカードを取得
            var selectedSoul1 = _soulCombiner.ownedSelectSouls.soulCardList[0];

            // 組み合わせ可能なソウルカードを探す
            var combinableSoul = _soulCombiner.SearchCombinableSoul(selectedSoul1);

            if (combinableSoul != null)
            {
                var resultSoul = _soulCombiner.combinations
                    .Find(c => c.IsValidCombination(selectedSoul1, combinableSoul))
                    .Result;

                Debug.Log($"ソウルカード「{selectedSoul1.SoulName}」と" +
                          $"組み合わせ可能なソウルカード「{combinableSoul.SoulName}」が見つかりました。");
                Debug.Log($"組み合わせた後のソウルカード：{resultSoul.SoulName}");
                Debug.Log("合成しますか？Y/N");

                if (await WaitForKeyDown(KeyCode.Y))
                {
                    Debug.Log("合成します");
                    var newSoul = _soulCombiner.Combine(selectedSoul1, combinableSoul);
                    _ownerCardList.soulCardList.Remove(selectedSoul1);
                    _ownerCardList.soulCardList.Remove(combinableSoul);
                    _ownerCardList.soulCardList.Add(newSoul);
                    Debug.Log($"新しいソウルカード「{newSoul.SoulName}」を作成しました");
                }
                else if (await WaitForKeyDown(KeyCode.N))
                {
                    Debug.Log("合成しません");
                }
            }
            else
            {
                Debug.Log("組み合わせ可能なソウルカードが見つかりませんでした。");
            }
            
            //レベルアップ処理
            //経験値の取得
            //経験値の計算
            
        }

        private static async UniTask<bool> WaitForKeyDown(KeyCode keyCode)
        {
            while (!Input.GetKeyDown(keyCode))
            {
                await UniTask.Yield();
            }

            return true;
        }
    }
}