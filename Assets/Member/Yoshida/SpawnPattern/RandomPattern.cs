using System.Collections.Generic;
using SoulRunProject.InGame;
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
        /// 円形範囲内のランダムな地点の座標を求めるメソッド
        /// </summary>
        /// <returns>円形範囲内のランダムな値</returns>
        float GetRandomValue()
        {
            var randomTheta = Random.Range(0, 360);
            var randomRange = Random.Range(0, _spawnRadiusRange);
            return randomRange * Mathf.Cos(randomTheta);
        }
        
        
        public void DrawGizmos(Vector3 pos)
        {
            // TODO 仮で相互参照　今度直す
            EntitySpawnController.DrawWireDisk(pos, _spawnRadiusRange, Color.red);
        }
    }
}