using System.Collections.Generic;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// 敵などのドロップ品の設定をするScriptableObject
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/Drop/LootTable")]
    public class LootTable : ScriptableObject
    {
        [Header("ドロップ")] 
        [SerializeField, CustomLabel("スコア")] private int _score;
        [SerializeField, CustomLabel("経験値")] private int _exp;
        [SerializeField, CustomLabel("コイン")] private int _coin;
        [SerializeField, CustomLabel("ソウルカードID")] private int _soulCardID;
        [SerializeField, CustomLabel("カードのドロップ率(百分率)")] private int _soulCardDropRate;
        [SerializeField, CustomLabel("カードに付与される経験値上限")] private int _soulCardExperienceAmount;

        public int SoulCardExperienceAmount => _soulCardExperienceAmount;

        public int Score => _score;

        public int Exp => _exp;

        public int Coin => _coin;

        public int SoulCardID => _soulCardID;

        public int SoulCardDropRate => _soulCardDropRate;
        // [SerializeField] List<LootPool> _pools;
        //
        // public List<DropData> Choose(Status status = null)
        // {
        //     List<DropData> dropData = new();
        //     foreach (var pool in _pools)
        //     {
        //         dropData.AddRange(pool.Choose(status));
        //     }
        //     return dropData;
        // }
    }
}