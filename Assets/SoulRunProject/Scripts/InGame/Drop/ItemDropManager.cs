using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 敵が死んだときにドロップするアイテムを管理するクラス
    /// </summary>
    public class ItemDropManager : AbstractSingletonMonoBehaviour<ItemDropManager>
    {
        [SerializeField] int _preloadCount = 5;
        [SerializeField] int _threshold = 5;
        readonly Dictionary<DropBase, DropPool> _dropPoolDictionary = new();
        public void Drop(LootTable lootTable, Vector3 pos, Status playerStatus = null)
        {
            foreach (var dropData in lootTable.Choose(playerStatus))
            {
                var pool = GetPool(dropData.Drop);
                for (int i = 0; i < dropData.Count; i++)
                {
                    var drop = pool.Rent();
                    drop.transform.position = pos;
                    drop.OnFinishedAsync.Take(1)
                        .Subscribe(_ =>
                        {
                            pool.Return(drop);
                        });
                }
            }
        }

        DropPool GetPool(DropBase key)
        {
            //  既に指定されたIDのpoolが存在していればそのpoolを返す
            if (_dropPoolDictionary.TryGetValue(key, out var value))
            {
                return value;
            }
            //  無ければ新しく生成
            var newParent = new GameObject().transform;
            newParent.name = key.name;
            newParent.SetParent(transform);
            _dropPoolDictionary.Add(key, new DropPool(newParent, key));
            _dropPoolDictionary[key].PreloadAsync(_preloadCount, _threshold).Subscribe();

            return _dropPoolDictionary[key];
        }

        protected override bool UseDontDestroyOnLoad { get; } = false;
    }
}
