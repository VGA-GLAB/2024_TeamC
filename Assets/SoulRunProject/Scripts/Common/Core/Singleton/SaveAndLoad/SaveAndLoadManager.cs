using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoulRunProject.SoulMixScene;
using UnityEngine;
using UnityEngine.Serialization;

namespace SoulRunProject.Common
{
    public class SaveAndLoadManager : AbstractSingletonMonoBehaviour<SaveAndLoadManager>
    {
        protected override bool UseDontDestroyOnLoad => true;

        private const string SaveFileName = "PlayerData.json";
        private const string MasterDataPath = "MasterData.json";

        private DataStorage _dataStorage;

        public override void OnAwake()
        {
            LoadPlayerDataFromJson();
        }

        private void OnDestroy()
        {
            SavePlayerDataToJson();
        }

        

        /// <summary>
        /// プレイヤーデータをJSONファイルから読み込む
        /// </summary>
        private void LoadPlayerDataFromJson()
        {
            string filePath = GetSaveFilePath();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                _dataStorage.playerData = JsonUtility.FromJson<PlayerData>(json);
            }
            else
            {
                _dataStorage.playerData = new PlayerData();
                _dataStorage.playerData.CurrentSoulCardDataList = new List<SoulCardMasterDataTable>();
                // その他のプレイヤーデータを初期化
            }
        }

        /// <summary>
        /// プレイヤーデータをJSONファイルに保存する
        /// </summary>
        private void SavePlayerDataToJson()
        {
            // プレイヤーデータをJSON形式の文字列に変換
            string json = JsonUtility.ToJson(_dataStorage.playerData, true);

            // ファイルの保存先パスを取得
            string filePath = GetSaveFilePath();

            // JSON文字列をファイルに書き込み
            File.WriteAllText(filePath, json);

            // セーブ完了のログを表示
            Debug.Log("Player data saved to " + filePath);
        }


        /// <summary>
        /// JSONファイルの保存先のパスを取得する
        /// </summary>
        /// <returns></returns>
        private string GetSaveFilePath()
        {
            return Path.Combine(Application.persistentDataPath, SaveFileName);
        }

        /// <summary>
        /// プレイヤーデータにSoulCardDataを追加する
        /// </summary>
        /// <param name="soulCardMasterDataData"></param>
        public void AddSoulCardToPlayerData(SoulCardMasterDataTable soulCardMasterDataData)
        {
            _dataStorage.playerData.CurrentSoulCardDataList.Add(soulCardMasterDataData);
        }

        /// <summary>
        /// プレイヤーデータからSoulCardDataを削除する
        /// </summary>
        /// <param name="soulCardMasterDataData"></param>
        public void RemoveSoulCardFromPlayerData(SoulCardMasterDataTable soulCardMasterDataData)
        {
            _dataStorage.playerData.CurrentSoulCardDataList.Remove(soulCardMasterDataData);
        }

        // その他のプレイヤーデータの操作メソッドを追加
        // 例：
        // public void AddItemToPlayerData(ItemData itemData) { ... }
        // public void RemoveItemFromPlayerData(ItemData itemData) { ... }

        // マスターデータとプレイヤーデータのアクセサを追加
        public MasterData GetMasterData()
        {
            return _dataStorage.masterData;
        }

        public PlayerData GetPlayerData()
        {
            return _dataStorage.playerData;
        }

        [System.Serializable]
        private class DataStorage
        {
            public MasterData masterData;
            public PlayerData playerData;
        }

        [System.Serializable]
        public class MasterData
        {
            public List<SoulCardMasterDataTable> soulCardDataList; // ソウルカードのマスターデータ

            public List<SoulCardMasterDataTable> soulCardDataCombinations; // ソウルカードの組み合わせのマスターデータ
            // エネミーのマスターデータ
            // アイテムのマスターデータ
        }

        [System.Serializable]
        public class PlayerData
        {
            public int MaxScore; //最高スコア
            public int CurrentMoney; //所持金
            public List<SoulCardMasterDataTable> CurrentSoulCardDataList; //所持しているもの
        }
    }
}



/*

{
  "PlayerDataID": "12345678AC",
  "PlayerName": "Hero",
  "Stage": {
    "1": 0,
    "2": 0
  },
  "CurrentMoney": 1500,
  "CurrentSoulCardDataList": [
    "A1B2C3D4E5",
    "F6G7H8I9J0"
  ]
}

{
  "Version": "0.1",
  "SoulCardDataList": [
    {
      "CardID": 1,
      "IndividualIdentificationNumber": "A1B2C3D4E5",
      "Image": "soul_card_1.png",
      "SoulName": "Fire Spirit",
      "SoulLevel": 1,
      "SoulAbility": {
        "AbilityName": "Flame Burst",
        "CoolTime": 10,
        "Effect": "Deal 100 damage to all enemies in range",
        "EffectText": "Unleashes a burst of flame, dealing damage to all enemies within range."
      },
      "Status": {
        "Hp": 100,
        "Attack": 20,
        "Defence": 10,
        "CoolTime": 0.5,
        "Range": 3.0,
        "BulletSpeed": 2.0,
        "EffectTime": 5.0,
        "BulletNum": 1,
        "Penetration": 0,
        "MoveSpeed": 1.5,
        "GrowthSpeed": 1.2,
        "Luck": 5
      },
      "TraitList": [
        {
          "Trait": "Fire Enhancement",
          "Effect": "Increases all fire damage by 20%",
          "EffectText": "This soul enhances the power of fire, increasing all fire damage dealt."
        }
      ]
    }
  ],
  "SoulCardDataCombinations": [
    {
      "Ingredient1": "A1B2C3D4E5",
      "Ingredient2": "F6G7H8I9J0",
      "Result": "K1L2M3N4O5"
    }
  ],
  "EnemyDataList": [
    {
      "EnemyID": 1,
      "EnemyImage": "enemy_1.png",
      "EnemyName": "Shadow Beast",
      "EnemyLevel": 5,
      "EnemyAbility": "Dark Swipe",
      "EnemyStatus": {
        "Hp": 150,
        "Attack": 25,
        "Defence": 5,
        "CoolTime": 2.0,
        "Range": 1.0,
        "BulletSpeed": 0,
        "EffectTime": 0,
        "BulletNum": 0,
        "Penetration": 0,
        "MoveSpeed": 1.0,
        "GrowthSpeed": 0,
        "Luck": 0
      }
    }
  ],
  "ItemDataList": [
    {
      "ItemID": 1,
      "ItemImage": "item_1.png",
      "ItemName": "Healing Potion",
      "Effect": "Restores 50 HP",
      "EffectText": "A magical potion that restores health."
    }
  ]
}



*/