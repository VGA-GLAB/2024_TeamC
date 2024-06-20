using System;
using System.Linq;
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

        public SoulCardUserData(int cardID, int experience)
        {
            CardID = cardID;
            Experience = experience;
        }
        
        ///追加特性
        //public CharacteristicType AdditionalCharacteristicType;
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

        public static bool TryCreateCard(int cardID , int exp , out SoulCard soulCard)
        {
            soulCard = null;
            if (MyRepository.Instance.TryGetData<SoulCardMasterDataTable>(out var table))
            {
                var cardMasterData = table.DataTable.FirstOrDefault(x => x.CardID == cardID);
                soulCard = new SoulCard(cardMasterData, new SoulCardUserData(cardID , exp));
                return true;
            }
            Debug.LogError("カードを生成できませんでした");
            return false;
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
        
        // /// <summary> 追加特性 </summary>
        // public CharacteristicType AdditionalCharacteristicType => _useSoulCardData.AdditionalCharacteristicType;

        public override int GetHashCode()
        {
            return CardID.GetHashCode()^ CurrentExperience.GetHashCode() ;
        }
        
        public bool Equals(SoulCard other)
        {
            if (other != null
                && this.CardID == other.CardID
                && this.CurrentExperience == other.CurrentExperience)
            {
                return true;
            }
            return false;
        }
    }


    

}
