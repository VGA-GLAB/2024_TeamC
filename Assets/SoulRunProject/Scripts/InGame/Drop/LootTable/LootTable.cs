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
        [SerializeField] List<LootPool> _pools;

        public List<DropData> Choose(Status status = null)
        {
            List<DropData> dropData = new();
            foreach (var pool in _pools)
            {
                dropData.AddRange(pool.Choose(status));
            }

            return dropData;
        }
    }
}