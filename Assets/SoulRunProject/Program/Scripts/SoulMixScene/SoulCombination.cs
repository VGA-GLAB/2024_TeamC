using System;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> 2つのソウルカードを組み合わせて新しいソウルカードを生成するためのクラス </summary>
    [System.Serializable]
    public class SoulCombination
    {
        [SerializeField] private SoulCard ingredient1; // 組み合わせる最初のソウルカード
        [SerializeField] private SoulCard ingredient2; // 組み合わせる2番目のソウルカード
        [SerializeField] private SoulCard result; // 組み合わせによって生成される新しいソウルカード

        public SoulCard Ingredient1
        {
            get => ingredient1;
            set => ingredient1 = value;
        }

        public SoulCard Ingredient2
        {
            get => ingredient2;
            set => ingredient2 = value;
        }

        public SoulCard Result
        {
            get => result;
            set => result = value;
        }

        /// <summary> この組み合わせが指定された2つのソウルカードと一致するかどうかを確認するメソッド </summary>
        public bool IsValidCombination(SoulCard soul1, SoulCard soul2)
        {
            return (soul1 == ingredient1 && soul2 == ingredient2) ||
                   (soul1 == ingredient2 && soul2 == ingredient1);
        }
    }
}