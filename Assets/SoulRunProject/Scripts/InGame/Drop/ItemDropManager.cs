using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 敵が死んだときにドロップするアイテムを管理するクラス
    /// </summary>
    public class ItemDropManager : AbstractSingletonMonoBehaviour<ItemDropManager>
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private GameObject _expSoulPrefab;
        
        public void DropCoin(Vector3 position)
        { 
            GameObject.Instantiate(_coinPrefab, position, Quaternion.identity);
        }

        protected override bool UseDontDestroyOnLoad { get; } = false;
    }
}
