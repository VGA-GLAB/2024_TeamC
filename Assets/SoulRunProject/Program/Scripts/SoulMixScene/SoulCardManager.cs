using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> ソウルを管理するクラス Controller </summary>
    public class SoulCardManager : MonoBehaviour
    {
        public List<SoulCard> soulCards = new List<SoulCard>();
        public TextAsset csvFile; // CSVファイルへの参照

        private const string SaveFileName = "PlayerData.json";

        private void Start()
        {
            LoadSoulCardsFromCSV();
            LoadPlayerDataFromJson();
        }

        private void OnDestroy()
        {
            SaveSoulCardsToScriptableObject();
            SavePlayerDataToJson();
        }

        public SoulCard CreateSoulCard(SoulCardData data)
        {
            GameObject soulCardObject = new GameObject("SoulCard");
            SoulCard soulCard = soulCardObject.AddComponent<SoulCard>();
            soulCard.SetData(data);
            soulCards.Add(soulCard);
            return soulCard;
        }

        private void SaveSoulCardsToScriptableObject()
        {
            foreach (SoulCard soulCard in soulCards)
            {
                SoulCardData data = soulCard.GetData();
                // ここでScriptableObjectに保存する処理を実装する
                // 例えば、Resources.SaveAssets()を使用するなど
            }
        }

        private void LoadSoulCardsFromCSV()
        {
            // CSVファイルからデータを読み込み、SoulCardを作成する処理を実装する
            // 例えば、CSVファイルをパースして、各行からSoulCardDataを作成し、CreateSoulCard()を呼び出すなど
        }

        private void SavePlayerDataToJson()
        {
            PlayerData playerData = new PlayerData();
            playerData.soulCardDataList = new List<SoulCardData>();

            foreach (SoulCard soulCard in soulCards)
            {
                playerData.soulCardDataList.Add(soulCard.GetData());
            }

            string json = JsonUtility.ToJson(playerData, true);
            File.WriteAllText(GetSaveFilePath(), json);
        }

        private void LoadPlayerDataFromJson()
        {
            string filePath = GetSaveFilePath();
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

                foreach (SoulCardData soulCardData in playerData.soulCardDataList)
                {
                    CreateSoulCard(soulCardData);
                }
            }
        }

        private string GetSaveFilePath()
        {
            return Path.Combine(Application.persistentDataPath, SaveFileName);
        }

        // 仮のプレイヤーデータを保存するためのクラス
        [System.Serializable]
        private class PlayerData
        {
            public List<SoulCardData> soulCardDataList;
        }
    }
}