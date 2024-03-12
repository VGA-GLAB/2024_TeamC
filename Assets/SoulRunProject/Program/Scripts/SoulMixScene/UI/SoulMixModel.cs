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
            // 所持しているソウルカードの出力
            foreach (var card in _ownerCardList.soulCardList)
            {
                Debug.Log($"{card.CardID}:{card.SoulName}");
            }

            // 選択されたソウルカードのリストが2つ以上であるか確認
            if (_soulCombiner.ownedSelectSouls.soulCardList.Count < 2)
            {
                Debug.Log("2つ以上のソウルカードを選択してください。");
                return;
            }

            // 選択された2つのソウルカードを取得
            var selectedSoul1 = _soulCombiner.ownedSelectSouls.soulCardList[0]; //最初に選択されたもの


            // もし組み合わせ可能なソウルカードが見つかったら
            if (_soulCombiner.IsSelectedSoul(selectedSoul1))
            {
                Debug.Log("組み合わせ可能なソウルカードが見つかりました。\n" +
                          "合成しますか？Y/N");
                if (await WaitForKeyDown(KeyCode.Y))
                {
                    Debug.Log("合成します");
                    // ソウルカードの組み合わせ可能リストを取得
                    var combineSoul = _soulCombiner.SearchCombineSoul(selectedSoul1);

                    // ソウルを合成し、新しいソウルカードを作成
                    var newSoul = _soulCombiner.Combine(selectedSoul1, combineSoul);

                    if (newSoul != null)
                    {
                        // 新しいソウルカードを所有者のリストに追加
                        _ownerCardList.soulCardList.Add(newSoul);
                        Debug.Log($"新しいソウルカードを作成しました: {newSoul.SoulName}");
                    }
                    else
                    {
                        Debug.Log("合成できるソウルカードが見つかりませんでした。");
                    }
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