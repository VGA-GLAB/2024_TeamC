using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using SoulRunProject.Common;
using UnityEngine.Serialization;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> ソウルカードのデータを管理するクラス </summary> 
    public class SoulCardManager
    {
        private readonly SoulCardList _soulCardAllList; // ゲームに登場する全てのソウルカード
        private readonly SoulMixModel _soulMixModel;
        private readonly SaveAndLoadManager _saveAndLoadManager;

        // コンストラクタインジェクションを使用して依存関係を注入
        public SoulCardManager(SoulCardList soulCardAllList, SoulMixModel soulMixModel,
            SaveAndLoadManager saveAndLoadManager)
        {
            _soulCardAllList = soulCardAllList;
            _soulMixModel = soulMixModel;
            _saveAndLoadManager = saveAndLoadManager;
            _soulMixModel.OnCardAdded.Subscribe(AddSoulCard);
            _soulMixModel.OnCardRemoved.Subscribe(RemoveSoulCard);
            // コンストラクタまたは初期化メソッド内でデータのロードを行う
            LoadSoulCards();
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