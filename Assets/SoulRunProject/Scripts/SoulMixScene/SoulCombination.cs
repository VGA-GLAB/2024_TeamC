using System;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> 2つのソウルカードを組み合わせて新しいソウルカードを生成するためのクラス </summary>
    [System.Serializable]
    public class SoulCombination
    {
        [SerializeField] private SoulCardData ingredient1; // 組み合わせる最初のソウルカード
        [SerializeField] private SoulCardData ingredient2; // 組み合わせる2番目のソウルカード
        [SerializeField] private SoulCardData result; // 組み合わせによって生成される新しいソウルカード

        public SoulCardData Ingredient1
        {
            get => ingredient1;
            set => ingredient1 = value;
        }

        public SoulCardData Ingredient2
        {
            get => ingredient2;
            set => ingredient2 = value;
        }

        public SoulCardData Result
        {
            get => result;
            set => result = value;
        }

        /// <summary> この組み合わせが指定された2つのソウルカードと一致するかどうかを確認するメソッド </summary>
        public bool IsValidCombination(SoulCardData soul1, SoulCardData soul2)
        {
            return (soul1 == ingredient1 && soul2 == ingredient2) ||
                   (soul1 == ingredient2 && soul2 == ingredient1);
        }

        public SoulCardData CombineResult(SoulCardData soul1, SoulCardData soul2)
        {
            return IsValidCombination(soul1, soul2) ? result : null;
        }
    }
}