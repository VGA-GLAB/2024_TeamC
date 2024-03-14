using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>円形範囲でランダムに生成位置を求めるクラス</summary>
    public class RandomPattern : ISpawnPattern
    {
        [SerializeField] int _spawnCount;
        [SerializeField] float _spawnRadiusRange;
        List<Vector3> _spawnPositions = new();
        
        /// <returns>生成位置のリスト</returns>
        public List<Vector3> GetSpawnPositions()
        {
            for (var i = 0; i < _spawnCount; i++)
            {
                _spawnPositions.Add(new Vector3(GetRandomValue(), 1, GetRandomValue()));
            }
            return _spawnPositions;
        }
        
        /// <summary>
        /// ランダムに
        /// </summary>
        /// <returns></returns>
        float GetRandomValue()
        {
            var randomTheta = Random.Range(0, 360);
            var randomRange = Random.Range(0, _spawnRadiusRange);
            return randomRange * Mathf.Cos(randomTheta);
        }
    }
}