using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    [CreateAssetMenu(fileName = "SoulCard", menuName = "SoulRunProject/SoulCard")]
    [Serializable]
    public class SoulCardData : ScriptableObject
    {
        // ソウルカードのID
        [SerializeField] private int cardID;

        public int CardID
        {
            get => cardID;
            set => cardID = value;
        }

        // ソウルカードの画像
        [SerializeField] private Sprite image;

        public Sprite Image
        {
            get => image;
            set => image = value;
        }

        // ソウルカードの名前
        [SerializeField] private string soulName;

        public string SoulName
        {
            get => soulName;
            set => soulName = value;
        }

        // ソウルカードのレベル
        [SerializeField] private int soulLevel;

        public int SoulLevel
        {
            get => soulLevel;
            set => soulLevel = value;
        }

        // ソウルカードの説明文
        [SerializeField] private SoulAbility soulAbility;

        public SoulAbility SoulAbility
        {
            get => soulAbility;
            set => soulAbility = value;
        }

        // ソウルカードの特性の説明文
        [SerializeField] private string explanatoryText;

        public string ExplanatoryText
        {
            get => explanatoryText;
            set => explanatoryText = value;
        }

        // ソウルカードのステータス
        [SerializeField] private Status status;

        public Status Status
        {
            get => status;
            set => status = value;
        }

        // ソウルカードの特性
        [SerializeField] private List<TraitWrapper> traitList;

        public List<TraitWrapper> TraitList
        {
            get => traitList;
            set => traitList = value;
        }
    }
}