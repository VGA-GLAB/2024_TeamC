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
        private const string MasterDataPath = "MasterData";

        private DataStorage _dataStorage;

        public override void OnAwake()
        {
            LoadMasterDataFromCSV();
            LoadPlayerDataFromJson();
        }

        private void OnDestroy()
        {
            SavePlayerDataToJson();
        }

        /// <summary>
        /// CSVファイルからマスターデータを読み込む
        /// </summary>
        private void LoadMasterDataFromCSV()
        {
            _dataStorage.masterData = new MasterData();

            // CSVファイルからソウルカードのマスターデータを読み込む
            TextAsset soulCardCsvFile = Resources.Load<TextAsset>(Path.Combine(MasterDataPath, "SoulCardMasterData"));
            _dataStorage.masterData.soulCardDataList = LoadSoulCardDataFromCSV(soulCardCsvFile);

            // その他のマスターデータのCSVファイルからデータを読み込む
            // 例：
            // TextAsset itemCsvFile = Resources.Load<TextAsset>(Path.Combine(MasterDataPath, "ItemMasterData"));
            // masterData.itemDataList = LoadItemDataFromCSV(itemCsvFile);
        }

        /// <summary>
        /// CSVファイルからSoulCardDataのリストを読み込む
        /// </summary>
        /// <param name="csvFile"></param>
        /// <returns></returns>
        private List<SoulCardMasterData> LoadSoulCardDataFromCSV(TextAsset csvFile)
        {
            List<SoulCardMasterData> dataList = new List<SoulCardMasterData>();
            string[] lines = csvFile.text.Split('\n');

            foreach (string line in lines.Skip(1)) // ヘッダー行をスキップ
            {
                string[] values = line.Split(',');
                // CSVの各列の値を取得し、SoulCardDataを作成
                // 例：
                // int cardID = int.Parse(values[0]);
                // string soulName = values[1];
                // ...
                SoulCardMasterData masterData = ScriptableObject.CreateInstance<SoulCardMasterData>();
                // データを設定
                dataList.Add(masterData);
            }

            return dataList;
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
                _dataStorage.playerData.CurrentSoulCardDataList = new List<SoulCardMasterData>();
                // その他のプレイヤーデータを初期化
            }
        }

        /// <summary>
        /// プレイヤーデータをJSONファイルに保存する
        /// </summary>
        private void SavePlayerDataToJson()
        {
            string json = JsonUtility.ToJson(_dataStorage.playerData, true);
            File.WriteAllText(GetSaveFilePath(), json);
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
        /// <param name="soulCardMasterData"></param>
        public void AddSoulCardToPlayerData(SoulCardMasterData soulCardMasterData)
        {
            _dataStorage.playerData.CurrentSoulCardDataList.Add(soulCardMasterData);
        }

        /// <summary>
        /// プレイヤーデータからSoulCardDataを削除する
        /// </summary>
        /// <param name="soulCardMasterData"></param>
        public void RemoveSoulCardFromPlayerData(SoulCardMasterData soulCardMasterData)
        {
            _dataStorage.playerData.CurrentSoulCardDataList.Remove(soulCardMasterData);
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
            public List<SoulCardMasterData> soulCardDataList; // ソウルカードのマスターデータ

            public List<SoulCardMasterData> soulCardDataCombinations; // ソウルカードの組み合わせのマスターデータ
            // エネミーのマスターデータ
            // アイテムのマスターデータ
        }

        [System.Serializable]
        public class PlayerData
        {
            public int MaxScore; //最高スコア
            public int CurrentMoney; //所持金
            public List<SoulCardMasterData> CurrentSoulCardDataList; //所持しているもの
        }
    }
}