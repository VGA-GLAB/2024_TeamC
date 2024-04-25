using System;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    public enum SoulSkillType
    {
        SoulFrame = 0,
    }
    /// <summary>
    /// ソウルカードのマスタデータ
    /// </summary>
    [CreateAssetMenu(fileName = "SoulCard", menuName = "SoulRunProject/SoulCard")]
    [Serializable]
    public class SoulCardMasterData : ScriptableObject
    {
        [SerializeField , CustomLabel("カードID")] private int _cardID;
        [SerializeField, CustomLabel("アイコン画像名")] private string _imageName;
        [SerializeField, CustomLabel("カード名")] private string _soulName;
        [SerializeField, CustomLabel("最大レベル")] private int _soulMaxLevel;
        [SerializeField, CustomLabel("説明文")] private string _description;
        [SerializeField, CustomLabel("ソウルスキル")] private SoulSkillType _soulSkillType;
        [SerializeField, CustomLabel("基礎ステータス")] private Status _baseStatus;
        [SerializeField, CustomLabel("固有特性")] private CharacteristicType _defaultCharacteristicType;

        public int CardID => _cardID;
        public string ImageName => _imageName;
        public string SoulName => _soulName;
        public int SoulMaxLevel => _soulMaxLevel;
        public string Description => _description;
        public SoulSkillType SoulSkillType => _soulSkillType;
        public Status BaseStatus => _baseStatus;
        public CharacteristicType DefaultCharacteristicType => _defaultCharacteristicType;
    }
}