// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UniRx;
// using SoulRunProject.Common;
// using UnityEngine.Serialization;
//
// namespace SoulRunProject.SoulMixScene
// {
//     /// <summary> ソウルカードのデータを管理するクラス </summary> 
//     public class SoulCardManager : AbstractSingletonMonoBehaviour<SoulCardManager>
//     {
//         protected override bool UseDontDestroyOnLoad => false; 
//         [SerializeField] private SoulMixModel _soulMixModel; // エディターから設定する
//
//         private SaveAndLoadManager _saveAndLoadManager;
//
//         private void Start()
//         {
//             _saveAndLoadManager = SaveAndLoadManager.Instance;
//             LoadSoulCards();
//             SaveAndLoadManager.MasterData masterData = _saveAndLoadManager.GetMasterData();
//             //soulCardListSO.soulCardList = masterData.soulCardDataList;
//         }
//
//         private void LoadSoulCards()
//         {
//             SaveAndLoadManager.PlayerData playerData = _saveAndLoadManager.GetPlayerData();
//
//             // PlayerDataからソウルカードをロードしてOwnedCardsに追加
//             foreach (SoulCardMasterDataTable soulCardData in playerData.CurrentSoulCardDataList)
//             {
//                 _soulMixModel.OwnedCards.Add(soulCardData);
//             }
//         }
//
//         // ソウルカードをリストに追加する処理は、OwnedCards.Addを直接使用
//         public void AddSoulCard(SoulCardMasterDataTable soulCardMasterData)
//         {
//             if (!_soulMixModel.OwnedCards.Contains(soulCardMasterData))
//             {
//                 _soulMixModel.OwnedCards.Add(soulCardMasterData);
//             }
//         }
//
//         // ソウルカードをリストから削除する処理は、OwnedCards.Removeを直接使用
//         public void RemoveSoulCard(SoulCardMasterDataTable soulCardMaster)
//         {
//             _soulMixModel.OwnedCards.Remove(soulCardMaster);
//         }
//
//         // IDでソウルカードを検索する処理
//         public SoulCardMasterData FindSoulCardByID(int cardID)
//         {
//             return _soulMixModel.OwnedCards.FirstOrDefault(card => card.CardID == cardID);
//         }
//     }
// }