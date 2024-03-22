using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using SoulRunProject.Common;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> ソウルカードのデータを管理するクラス </summary> 
    public class SoulCardManager : AbstractSingletonMonoBehaviour<SoulCardManager>
    {
        protected override bool UseDontDestroyOnLoad => false;
        public SoulCardList soulCardListSO; // ゲームに登場する全てのソウルカード
        public SoulMixModel soulMixModel; // エディターから設定する

        private SaveAndLoadManager _saveAndLoadManager;

        private void Start()
        {
            _saveAndLoadManager = SaveAndLoadManager.Instance;
            LoadSoulCards();
            SaveAndLoadManager.MasterData masterData = _saveAndLoadManager.GetMasterData();
            //soulCardListSO.soulCardList = masterData.soulCardDataList;
        }

        private void LoadSoulCards()
        {
            SaveAndLoadManager.PlayerData playerData = _saveAndLoadManager.GetPlayerData();

            // PlayerDataからソウルカードをロードしてOwnedCardsに追加
            foreach (SoulCardData soulCardData in playerData.CurrentSoulCardDataList)
            {
                soulMixModel.OwnedCards.Add(soulCardData);
            }
        }

        // ソウルカードをリストに追加する処理は、OwnedCards.Addを直接使用
        public void AddSoulCard(SoulCardData soulCardData)
        {
            if (!soulMixModel.OwnedCards.Contains(soulCardData))
            {
                soulMixModel.OwnedCards.Add(soulCardData);
            }
        }

        // ソウルカードをリストから削除する処理は、OwnedCards.Removeを直接使用
        public void RemoveSoulCard(SoulCardData soulCard)
        {
            soulMixModel.OwnedCards.Remove(soulCard);
        }

        // IDでソウルカードを検索する処理
        public SoulCardData FindSoulCardByID(int cardID)
        {
            return soulMixModel.OwnedCards.FirstOrDefault(card => card.CardID == cardID);
        }
    }
}