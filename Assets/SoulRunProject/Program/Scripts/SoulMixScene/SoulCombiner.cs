using System;
using System.Collections.Generic;
using SoulRun.InGame;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> 組み合わせを判定するクラス </summary>
    public class SoulCombiner : MonoBehaviour
    {
        public SoulCardList ownedSouls; // 所持しているソウルのリスト
        public List<SoulCombination> combinations; // 使用可能な配合表のリスト

        public InputUIButton soul1Button; // ソウル1のボタン

        private void Awake()
        {
            TryGetComponent(out soul1Button);
            soul1Button.OnClick.AddListener(SelectSoul1);
        }

        // ソウル1を選択するメソッド
        private void SelectSoul1(InputUIButton button)
        {
            Debug.Log("SelectSoul1");
        }

        // 2つのソウルを組み合わせて新しいソウルを作成するメソッド
        public SoulCard Combine(SoulCard soul1, SoulCard soul2)
        {
            foreach (var combination in combinations)
            {
                if (combination.IsValidCombination(soul1, soul2))
                {
                    // ソウルカードのリストから削除
                    ownedSouls.soulCardList.Remove(soul1);
                    ownedSouls.soulCardList.Remove(soul2);

                    // 新しいソウルカードの作成
                    // ここで、combination.result は新しいソウルカードのプロトタイプを指すと仮定しています。
                    // 新しいインスタンスを作成する方法は、プロジェクトの設計によります。
                    SoulCard newSoul = Instantiate(combination.result);

                    // 新しいソウルカードをリストに追加
                    ownedSouls.soulCardList.Add(newSoul);
                    return newSoul; // 新しいソウルカードを返す
                }
            }

            return null; // 有効な組み合わせがない場合はnullを返す
        }
    }
}