using System;
using SoulRunProject.InGame;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// LootPoolで抽選するための重みやドロップ品、出力する個数を設定するためのクラス。
    /// </summary>
    [Serializable]
    public class LootEntry
    {
        [SerializeField, Tooltip("ドロップ品")] DropBase _drop;
        [SerializeReference, SubclassSelector, Tooltip("ドロップ数")] ICount _count;
        [SerializeField, Tooltip("ドロップ数に加算するLuck値の乗数")] float _countQuality = 0;
        [SerializeField, Tooltip("重み")] int _weight = 1;
        [SerializeField, Tooltip("重みに加算するLuck値の乗数")] float _weightQuality = 0;
        public DropBase Drop => _drop;

        /// <summary>プレイヤーのLuckが考慮された最終的なドロップ数</summary>
        public int GetCount(Status status = null)
        {
            var count = 1;
            if (_count != null)
            {
                count = _count.GetCount;
            }
            if (status == null)
            {
                return count;
            }
            return (int)(count + _countQuality * status.Luck);
        }
        /// <summary>プレイヤーのLuckが考慮された最終的な重み</summary>
        public int GetWeight(Status status = null)
        {
            if (status == null)
            {
                return _weight;
            }
            return (int)(_weight + _weightQuality * status.Luck);
        }
    }
}