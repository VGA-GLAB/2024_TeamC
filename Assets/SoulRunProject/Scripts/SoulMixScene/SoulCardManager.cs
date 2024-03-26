using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using SoulRunProject.Common;
using UnityEngine.Serialization;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> ソウルカードのデータを管理するクラス </summary> 
    public class SoulCardManager : AbstractSingletonMonoBehaviour<SoulCardManager>
    {
        protected override bool UseDontDestroyOnLoad => false;
        [SerializeField] private SoulCardList _soulCardListSo; // ゲームに登場する全てのソウルカード
        [SerializeField] private SoulMixModel _soulMixModel; // エディターから設定する

        private SaveAndLoadManager _saveAndLoadManager;
        SaveAndLoadManager.MasterData _masterData;

        private void Start()
        {
            _saveAndLoadManager = SaveAndLoadManager.Instance;
            LoadSoulCards();
            _masterData = _saveAndLoadManager.GetMasterData();
            //soulCardListSO.soulCardList = masterData.soulCardDataList;
        }

        private void LoadSoulCards()
        {
            SaveAndLoadManager.PlayerData playerData = _saveAndLoadManager.GetPlayerData();

            // PlayerDataからソウルカードをロードしてOwnedCardsに追加
            foreach (SoulCardData soulCardData in playerData.CurrentSoulCardDataList)
            {
                _soulMixModel.OwnedCards.Add(soulCardData);
            }
        }

        // ソウルカードをリストに追加する処理は、OwnedCards.Addを直接使用
        public void AddSoulCard(SoulCardData soulCardData)
        {
            if (!_soulMixModel.OwnedCards.Contains(soulCardData))
            {
                _soulMixModel.OwnedCards.Add(soulCardData);
            }
        }

        // ソウルカードをリストから削除する処理は、OwnedCards.Removeを直接使用
        public void RemoveSoulCard(SoulCardData soulCard)
        {
            _soulMixModel.OwnedCards.Remove(soulCard);
        }

        // IDでソウルカードを検索する処理
        public SoulCardData FindSoulCardByID(int cardID)
        {
            return _soulMixModel.OwnedCards.FirstOrDefault(card => card.CardID == cardID);
        }
    }
}