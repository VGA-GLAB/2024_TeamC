using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.InGame.SpawnPattern
{
    public interface ISpawnPattern
    {
        public List<Vector3> GetSpawnPositions(int spawnCount);
    }
}