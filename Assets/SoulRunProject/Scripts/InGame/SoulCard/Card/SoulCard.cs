using System;
using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject
{
    [Serializable]
    public class SoulCardUserData 
    {
        /// <summary> ユニークなカード識別ID </summary>
        public int CardID;
        
        /// <summary> 現在の経験値量 </summary>
        public int Experience;

        /// <summary> 特性 </summary>
        public CharacteristicType AdditionalCharacteristicType;
    }


    public class SoulCard　: IEquatable<SoulCard>
    {
        private readonly SoulCardMasterData _masterSoulCardData;
        private readonly SoulCardUserData _useSoulCardData;

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
        
        /// <summary> 基本特性 </summary>
        public SoulCardExperienceTableType ExperienceTableType => _masterSoulCardData.ExperienceTableType;

        /// <summary> 基本特性 </summary>
        public SoulCardStatusTableType StatusTableType => _masterSoulCardData.StatusTableType;
        
        /// <summary> 基本特性 </summary>
        public CharacteristicType DefaultCharacteristicType => _masterSoulCardData.DefaultCharacteristicType;
        
        
        /// <summary> 現在の経験値量 </summary>
        public int CurrentExperience => _useSoulCardData.Experience;
        
        /// <summary> 追加特性 </summary>
        public CharacteristicType AdditionalCharacteristicType => _useSoulCardData.AdditionalCharacteristicType;

        public override int GetHashCode()
        {
            return CardID.GetHashCode()^ CurrentExperience.GetHashCode() ^ AdditionalCharacteristicType.GetHashCode();
        }
        
        public bool Equals(SoulCard other)
        {
            if (other != null
                && this.CardID == other.CardID
                && this.CurrentExperience == other.CurrentExperience
                && this.AdditionalCharacteristicType == other.AdditionalCharacteristicType)
            {
                return true;
            }
            return false;
        }
    }


    

}
