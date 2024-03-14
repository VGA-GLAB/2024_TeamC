using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.Common
{
    public class InvertedTrianglePattern : ISpawnPattern
    {
        [SerializeField] int _spawnRowCount;
        List<Vector3> _spawnPositions = new();

        /// <returns>生成位置のリスト</returns>
        public List<Vector3> GetSpawnPositions()
        {
            var invertedRowCount = _spawnRowCount + 1;
            // 最初の1列はここで追加
            _spawnPositions.Add(new Vector3(0, 0, invertedRowCount));
            // TODO 列指定の下限や上限があるのか決める
            if (_spawnRowCount <= 1)
            {
                return _spawnPositions;
            }

            // 2列目からの生成位置を求めるループ
            for (var i = 0; i < _spawnRowCount; i++)
            {
                var value = -i;
                for (var j = 0; j <= i; j++)
                {
                    // 各列左端は基準になるので、処理を分ける
                    if (j <= 0)
                    {
                        var poss = new Vector3(value, 0,  invertedRowCount - i);
                        _spawnPositions.Add(poss);
                        continue;
                    }

                    var pos = new Vector3(value += 2, 0, invertedRowCount - i);
                    _spawnPositions.Add(pos);
                }
            }

            return _spawnPositions;
        }
    }
}