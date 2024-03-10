using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// 生成パターンのインターフェース
    /// </summary>
    public interface ISpawnPattern
    {
        public List<Vector3> GetSpawnPositions();
    }
}