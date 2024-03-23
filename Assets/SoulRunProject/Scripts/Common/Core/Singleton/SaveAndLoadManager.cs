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
        private List<SoulCardData> LoadSoulCardDataFromCSV(TextAsset csvFile)
        {
            List<SoulCardData> dataList = new List<SoulCardData>();
            string[] lines = csvFile.text.Split('\n');

            foreach (string line in lines.Skip(1)) // ヘッダー行をスキップ
            {
                string[] values = line.Split(',');
                // CSVの各列の値を取得し、SoulCardDataを作成
                // 例：
                // int cardID = int.Parse(values[0]);
                // string soulName = values[1];
                // ...
                SoulCardData data = ScriptableObject.CreateInstance<SoulCardData>();
                // データを設定
                dataList.Add(data);
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
                _dataStorage.playerData.CurrentSoulCardDataList = new List<SoulCardData>();
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
        /// <param name="soulCardData"></param>
        public void AddSoulCardToPlayerData(SoulCardData soulCardData)
        {
            _dataStorage.playerData.CurrentSoulCardDataList.Add(soulCardData);
        }

        /// <summary>
        /// プレイヤーデータからSoulCardDataを削除する
        /// </summary>
        /// <param name="soulCardData"></param>
        public void RemoveSoulCardFromPlayerData(SoulCardData soulCardData)
        {
            _dataStorage.playerData.CurrentSoulCardDataList.Remove(soulCardData);
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
            public List<SoulCardData> soulCardDataList; // ソウルカードのマスターデータ

            public List<SoulCardData> soulCardDataCombinations; // ソウルカードの組み合わせのマスターデータ
            // エネミーのマスターデータ
            // アイテムのマスターデータ
        }

        [System.Serializable]
        public class PlayerData
        {
            public int MaxScore; //最高スコア
            public int CurrentMoney; //所持金
            public List<SoulCardData> CurrentSoulCardDataList; //所持しているもの
        }
    }
}