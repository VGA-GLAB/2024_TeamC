using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunProject.SoulMixScene
{
    [CreateAssetMenu(fileName = "SoulCard", menuName = "SoulRunProject/SoulCard")]
    [Serializable]
    public class SoulCardData : ScriptableObject
    {
        // ソウルカードのID
        [SerializeField] private int _uniqueUniqueCardID;

        public int UniqueCardID
        {
            get => _uniqueUniqueCardID;
            set => _uniqueUniqueCardID = value;
        }

        // ソウルカードの個体識別番号
        [SerializeField] private string _individualIdentificationNumber;

        public string IndividualIdentificationNumber
        {
            get => _individualIdentificationNumber;
            set => _individualIdentificationNumber = value;
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

        // ソウルカードのアビリティ
        [SerializeField] private SoulAbility soulAbility;

        public SoulAbility SoulAbility
        {
            get => soulAbility;
            set => soulAbility = value;
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