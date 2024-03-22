using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>列を指定し、二等辺三角形状で等間隔に生成位置を求めるクラス</summary>
    public class TrianglePattern : ISpawnPattern
    {
        [SerializeField] int _spawnRowCount;
        [SerializeField] float _entitySpacing;
        List<Vector3> _spawnPositions = new();

        /// <returns>生成位置のリスト</returns>
        public List<Vector3> GetSpawnPositions()
        {
            // 最初の1列はここで追加
            _spawnPositions.Add(new Vector3(0, 0, 0));
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
                        _spawnPositions.Add(new Vector3(xPos, 0, zPos));
                        continue;
                    }

                    var pos = new Vector3(xPos += (_entitySpacing * 2), 0, zPos);
                    _spawnPositions.Add(pos);
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