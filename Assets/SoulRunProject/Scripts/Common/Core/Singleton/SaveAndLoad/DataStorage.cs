using System.Collections.Generic;
using SoulRunProject.SoulMixScene;

namespace SoulRunProject.Common
{
    // データベースのバージョン情報を持つ基本クラス
    public class DataBase
    {
        public string version;
    }

    // マスターデータの定義
    [System.Serializable]
    public class MasterData : DataBase
    {
        public List<SoulCardMasterData> soulCardDataList; // ソウルカードのマスターデータ

        public List<SoulCardMasterData> soulCardDataCombinations; // ソウルカードの組み合わせのマスターデータ
        // ここにエネミーのマスターデータとアイテムのマスターデータを追加する場所
    }

    // プレイヤーデータの定義
    [System.Serializable]
    public class PlayerData : DataBase
    {
        public int MaxScore; // 最高スコア
        public int CurrentMoney; // 所持金
        public List<SoulCardMasterData> CurrentSoulCardDataList; // 所持しているソウルカードのリスト
    }

    // データの保管とアクセスを管理
    [System.Serializable]
    public class DataStorage
    {
        private PlayerData _playerData;
        private MasterData _masterData;

        // プレイヤーデータのプロパティ
        public PlayerData PlayerData
        {
            get { return _playerData; }
            set { _playerData = value; }
        }

        // マスターデータのプロパティ
        public MasterData MasterData
        {
            get { return _masterData; }
            set { _masterData = value; }
        }
    }
}