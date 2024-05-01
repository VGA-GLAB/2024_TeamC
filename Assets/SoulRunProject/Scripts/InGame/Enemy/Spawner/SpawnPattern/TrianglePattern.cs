using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.Common.Interface
{
    /// <summary>列を指定し、二等辺三角形状で等間隔に生成位置を求めるクラス</summary>
    [Serializable, Name("三角形スポーン")]
    public class TrianglePattern : ISpawnPattern
    {
        [SerializeField, CustomLabel("生成する行数"), Range(0, 100)]
        int _spawnRowCount;

        [SerializeField, CustomLabel("生成間隔"), Range(0, 5)]
        float _entitySpacing;

        [SerializeField, CustomLabel("生成向き (オイラー角)"), Range(0, 90)]
        float _spawnAngle;

        List<(Vector3, float)> _spawnPositions = new();

        /// <returns>生成位置のリスト</returns>
        public List<(Vector3, float)> GetSpawnPositions()
        {
            // 最初の1列はここで追加
            _spawnPositions.Add((new Vector3(0, 0, 0), _spawnAngle));
            // TODO 列指定の下限や上限があるのか決める
            if (_spawnRowCount <= 1)
            {
                return _spawnPositions;
            }

            // 2列目からの生成位置を求めるループ
            for (var i = 1; i < _spawnRowCount; i++)
            {
                var xPos = -i * _entitySpacing;
                var zPos = i * _entitySpacing;
                for (var j = 0; j <= i; j++)
                {
                    // 各列左端は基準になるので、処理を分ける
                    if (j <= 0)
                    {
                        _spawnPositions.Add((new Vector3(xPos, 0, zPos), _spawnAngle));
                        continue;
                    }

                    var pos = new Vector3(xPos += (_entitySpacing * 2), 0, zPos);
                    _spawnPositions.Add((pos, _spawnAngle));
                }
            }

            return _spawnPositions;
        }

        public void DrawGizmos(Vector3 pos)
        {
            // ignore
        }
    }
}