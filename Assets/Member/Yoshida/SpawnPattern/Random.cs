using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.InGame.SpawnPattern
{
    public class Random : ISpawnPattern
    {
        [SerializeField] float _spawnRadiusRange;
        List<Vector3> _spawnPositions;
        
        public List<Vector3> GetSpawnPositions(int spawnCount)
        {
            for (var i = 0; i < spawnCount; i++)
            {
                _spawnPositions.Add(new Vector3(GetRandomValue(), 1, GetRandomValue()));
            }
            return _spawnPositions;

            float GetRandomValue()
            {
                var randomTheta = UnityEngine.Random.Range(0, 360);
                var randomRange = UnityEngine.Random.Range(0, _spawnRadiusRange);
                return randomRange * Mathf.Cos(randomTheta);
            }
        }
    }
}