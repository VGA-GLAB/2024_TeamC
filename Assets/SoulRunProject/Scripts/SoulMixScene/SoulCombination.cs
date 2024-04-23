using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> 2つのソウルカードを組み合わせて新しいソウルカードを生成するためのクラス </summary>
    [CreateAssetMenu(fileName = "SoulCombination", menuName = "SoulRunProject/SoulCombination")]
    public class SoulCombination : ScriptableObject
    {
        [SerializeField] private SoulCardMasterData _ingredient1; // 組み合わせる最初のソウルカード
        [SerializeField] private SoulCardMasterData _ingredient2; // 組み合わせる2番目のソウルカード
        [SerializeField] private SoulCardMasterData _result; // 組み合わせによって生成される新しいソウルカード

        public SoulCardMasterData Ingredient1
        {
            get => _ingredient1;
            set => _ingredient1 = value;
        }

        public SoulCardMasterData Ingredient2
        {
            get => _ingredient2;
            set => _ingredient2 = value;
        }

        public SoulCardMasterData Result
        {
            get => _result;
            set => _result = value;
        }

        /// <summary> この組み合わせが指定された2つのソウルカードと一致するかどうかを確認するメソッド </summary>
        public bool IsValidCombination(SoulCardMasterData soul1, SoulCardMasterData soul2)
        {
            return (soul1 == _ingredient1 && soul2 == _ingredient2) ||
                   (soul1 == _ingredient2 && soul2 == _ingredient1);
        }

        public SoulCardMasterData CombineResult(SoulCardMasterData soul1, SoulCardMasterData soul2)
        {
            return IsValidCombination(soul1, soul2) ? _result : null;
        }
    }
}