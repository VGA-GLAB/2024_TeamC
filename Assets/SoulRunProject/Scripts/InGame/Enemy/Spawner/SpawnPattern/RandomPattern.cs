using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulRunProject.Common.Interface
{
    /// <summary>円形範囲でランダムに生成位置を求めるクラス</summary>
    [Serializable, Name("円形ランダムスポーン")]
    public class RandomPattern : ISpawnPattern
    {
        [SerializeField, CustomLabel("生成数"), Range(0, 100)]
        int _spawnCount;

        [SerializeField, CustomLabel("生成範囲"), Range(0, 100)]
        float _spawnRadiusRange;

        [SerializeField, CustomLabel("生成向きのランダム化")]
        bool _useRandomAngle;

        List<(Vector3, float)> _spawnPositions = new();

        /// <returns>生成位置のリスト</returns>
        public List<(Vector3, float)> GetSpawnPositions()
        {
            var spawnAngle = 0f;
            for (var i = 0; i < _spawnCount; i++)
            {
                if (_useRandomAngle)
                {
                    spawnAngle = Random.Range(-60f, 60f);
                }

                _spawnPositions.Add((new Vector3(GetRandomValue(), 1, GetRandomValue()), spawnAngle));
            }

            return _spawnPositions;
        }

        public void DrawGizmos(Vector3 pos)
        {
            GizmoDrawWireDisk.DrawWireDisk(pos, _spawnRadiusRange, Color.red);
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

        // public void DrawGizmos(Vector3 pos)
        // {
        //     // TODO 仮で相互参照　今度直す
        //     EntitySpawnerController.DrawWireDisk(pos, _spawnRadiusRange, Color.red);
        // }
    }
}