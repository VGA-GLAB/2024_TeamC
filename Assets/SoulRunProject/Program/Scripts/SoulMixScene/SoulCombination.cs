﻿namespace SoulRunProject.SoulMixScene
{
    [System.Serializable]
    public class SoulCombination
    {
        public SoulCard ingredient1; // 組み合わせる最初のソウルカード
        public SoulCard ingredient2; // 組み合わせる2番目のソウルカード
        public SoulCard result;       // 組み合わせによって生成される新しいソウルカード

        // この組み合わせが指定された2つのソウルカードと一致するかどうかを確認するメソッド
        public bool IsValidCombination(SoulCard soul1, SoulCard soul2)
        {
            return (soul1 == ingredient1 && soul2 == ingredient2) || 
                   (soul1 == ingredient2 && soul2 == ingredient1);
        }
    }
}