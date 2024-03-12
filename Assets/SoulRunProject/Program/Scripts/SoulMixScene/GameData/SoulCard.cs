using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    public class SoulCard : MonoBehaviour
    {
        public SoulCardData soulCardData;

        // ここで、soulCardDataを使用してソウルカードの情報を取得したり、設定したりします。
        public int CardID
        {
            get => soulCardData.CardID;
            set => soulCardData.CardID = value;
        }

        public Sprite Image
        {
            get => soulCardData.Image;
            set => soulCardData.Image = value;
        }

        public string SoulName
        {
            get => soulCardData.SoulName;
            set => soulCardData.SoulName = value;
        }

        public int SoulLevel
        {
            get => soulCardData.SoulLevel;
            set => soulCardData.SoulLevel = value;
        }

        public SoulAbility SoulAbility
        {
            get => soulCardData.SoulAbility;
            set => soulCardData.SoulAbility = value;
        }

        public string ExplanatoryText
        {
            get => soulCardData.ExplanatoryText;
            set => soulCardData.ExplanatoryText = value;
        }

        public Status Status
        {
            get => soulCardData.Status;
            set => soulCardData.Status = value;
        }

        public List<TraitWrapper> TraitList
        {
            get => soulCardData.TraitList;
            set => soulCardData.TraitList = value;
        }

        public void SetData(SoulCardData data)
        {
            CardID = data.CardID;
            Image = data.Image;
            SoulName = data.SoulName;
            SoulLevel = data.SoulLevel;
            SoulAbility = data.SoulAbility;
            ExplanatoryText = data.ExplanatoryText;
            Status = data.Status;
            TraitList = data.TraitList;
        }

        public SoulCardData GetData()
        {
            SoulCardData data = ScriptableObject.CreateInstance<SoulCardData>();
            data.CardID = CardID;
            data.Image = Image;
            data.SoulName = SoulName;
            data.SoulLevel = SoulLevel;
            data.SoulAbility = SoulAbility;
            data.ExplanatoryText = ExplanatoryText;
            data.Status = Status;
            data.TraitList = TraitList;
            return data;
        }
    }
}