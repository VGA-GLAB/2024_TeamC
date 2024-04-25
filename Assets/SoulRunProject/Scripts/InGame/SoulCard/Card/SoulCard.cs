using System;
using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject
{
    public class SoulCard　: IEquatable<SoulCard>
    {
        private SoulCardMasterData _masterSoulCardData;
        private SoulCardUserData _useSoulCardData;

        public SoulCard(SoulCardMasterData masterSoulCardData, SoulCardUserData useSoulCardData)
        {
            _masterSoulCardData = masterSoulCardData;
            _useSoulCardData = useSoulCardData;
        }
        
        /// <summary> ユニークなカード識別ID </summary>
        public int CardID => _masterSoulCardData.CardID;
        
        /// <summary> カードイメージ </summary>
        public string ImageName => _masterSoulCardData.ImageName;
        
        /// <summary> カード名 </summary>
        public string SoulName => _masterSoulCardData.SoulName;
        
        /// <summary> スキル動作クラス </summary>
        public SoulSkillType SoulSkill => _masterSoulCardData.SoulSkillType;
        
        /// <summary> カードの基礎上昇パラメータ </summary>
        public Status BaseStatus => _masterSoulCardData.BaseStatus;
        
        /// <summary> 最大レベル </summary>
        public int SoulMaxLevel => _masterSoulCardData.SoulMaxLevel;
        
        /// <summary> 基本特性 </summary>
        public CharacteristicType DefaultCharacteristicType => _masterSoulCardData.DefaultCharacteristicType;
        
        // <summary> 現在のレベル </summary>
        public int CurrentLevel => _useSoulCardData.CurrentLevel;
        
        /// <summary> 現在の経験値量 </summary>
        public int CurrentExperience => _useSoulCardData.Experience;
        
        /// <summary> 追加特性 </summary>
        public CharacteristicType AdditionalCharacteristicType => _useSoulCardData.AdditionalCharacteristicType;

        public override int GetHashCode()
        {
            return CardID.GetHashCode() ^ CurrentLevel.GetHashCode() ^ CurrentExperience.GetHashCode() ^ AdditionalCharacteristicType.GetHashCode();
        }
        
        public bool Equals(SoulCard other)
        {
            if (this.CardID == other.CardID
                && this.CurrentLevel == other.CurrentLevel
                && this.CurrentExperience == other.CurrentExperience
                && this.AdditionalCharacteristicType == other.AdditionalCharacteristicType)
            {
                return true;
            }
            return false;
        }
    }


    
    [Serializable]
    public class SoulCardUserData 
    {
        /// <summary> ユニークなカード識別ID </summary>
        public int CardID;

        /// <summary> 現在のレベル </summary>
        public int CurrentLevel;

        /// <summary> 現在の経験値量 </summary>
        public int Experience;

        /// <summary> 特性 </summary>
        public CharacteristicType AdditionalCharacteristicType;
    }

    /// <summary>
    /// ソウルカードの特性タイプ
    /// </summary>
    public enum CharacteristicType
    {
        None = 0,
        Attack = 1,
        CoolTimeReduction = 2,
    }
}
