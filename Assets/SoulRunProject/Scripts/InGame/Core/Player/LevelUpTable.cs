using System;
using System.Collections.Generic;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// レベルアップテーブルクラス
    /// </summary>
    [Serializable]
    public class LevelUpTable
    {
        public List<LevelData> Table = new List<LevelData>();
    }
    
    /// <summary>
    /// 各レベルのデータ
    /// </summary>
    [Serializable]
    public class LevelData
    {
        public int CurrentLevel;
        public int ExpToNextLevel;
    }
}
