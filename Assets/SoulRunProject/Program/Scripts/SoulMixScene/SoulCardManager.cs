using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using SoulRunProject.Common;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> ソウルを管理するクラス Controller </summary>
    public class SoulCardManager : MonoBehaviour
    {
        public List<SoulCard> soulCards = new List<SoulCard>();

        private SaveAndLoadManager _saveAndLoadManager;

        private void Start()
        {
            _saveAndLoadManager = SaveAndLoadManager.Instance;
            LoadSoulCards();
        }

        /// <summary> ソウルカードをロードする </summary>
        private void LoadSoulCards()
        {
            SaveAndLoadManager.PlayerData playerData = _saveAndLoadManager.GetPlayerData();

            foreach (SoulCardData soulCardData in playerData.soulCardDataList)
            {
                CreateSoulCard(soulCardData);
            }
        }

        /// <summary> ソウルカードを作成する </summary>
        public SoulCard CreateSoulCard(SoulCardData data)
        {
            GameObject soulCardObject = new GameObject();
            SoulCard soulCard = soulCardObject.AddComponent<SoulCard>();
            soulCard.SetData(data);
            soulCard.name = data.SoulName;
            soulCards.Add(soulCard);
            return soulCard;
        }

        /// <summary> ソウルカードを追加する </summary>
        public void AddSoulCard(SoulCardData soulCardData)
        {
            SoulCard soulCard = CreateSoulCard(soulCardData);
            _saveAndLoadManager.AddSoulCardToPlayerData(soulCardData);
        }

        /// <summary> ソウルカードを削除する </summary>
        public void RemoveSoulCard(SoulCard soulCard)
        {
            soulCards.Remove(soulCard);
            _saveAndLoadManager.RemoveSoulCardFromPlayerData(soulCard.GetData());
            Destroy(soulCard.gameObject);
        }

        /// <summary> ソウルカードをIDで検索する </summary>
        public SoulCard FindSoulCardByID(int cardID)
        {
            return soulCards.FirstOrDefault(card => card.CardID == cardID);
        }
    }
}