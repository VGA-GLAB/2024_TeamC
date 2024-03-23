using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixModel : MonoBehaviour
    {
        [SerializeField] private SoulCombiner _soulCombiner;
        public ReactiveCollection<SoulCardData> OwnedCards = new ReactiveCollection<SoulCardData>();
        private readonly SoulCardManager _soulCardManager = SoulCardManager.Instance;

        // ログメッセージを発行するためのReactiveProperty
        public ReactiveProperty<string> logMessage = new ReactiveProperty<string>();

        public async UniTaskVoid SoulMixAsync()
        {
            // 選択されたソウルカードのリストが2つ以上であるか確認
            if (_soulCombiner.ownedSelectSouls.soulCardList.Count < 2)
            {
                logMessage.Value = "ソウルカードを2つ以上選択してください。";
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

                logMessage.Value = $"ソウルカード「{selectedSoul1.SoulName}」と" +
                                   $"組み合わせ可能なソウルカード「{combinableSoul.SoulName}」が見つかりました。";
                logMessage.Value = $"組み合わせた後のソウルカード：{resultSoul.SoulName}";
                logMessage.Value = "合成しますか？Y/N";

                if (await WaitForKeyDown(KeyCode.Y))
                {
                    logMessage.Value = "合成します";
                    var newSoul = _soulCombiner.Combine(selectedSoul1, combinableSoul);
                    _soulCardManager.RemoveSoulCard(selectedSoul1);
                    _soulCardManager.RemoveSoulCard(combinableSoul);
                    _soulCardManager.AddSoulCard(newSoul);
                    logMessage.Value = $"新しいソウルカード「{newSoul.SoulName}」を作成しました";
                }
                else if (await WaitForKeyDown(KeyCode.N))
                {
                    logMessage.Value = "合成しません";
                }
            }
            else
            {
                logMessage.Value = "組み合わせ可能なソウルカードが見つかりませんでした。";
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