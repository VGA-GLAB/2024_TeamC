using UnityEngine;
using Random = UnityEngine.Random;

namespace SoulRunProject.TakechiTest
{
    /// <summary>
    /// デバッグ用スポナークラス
    /// </summary>
    public class TestEnemySpawner : MonoBehaviour
    {
        [SerializeField] TestEnemy _enemy;
        [SerializeField] TestPlayerManager _playerManager;
        [SerializeField] float _maxSpawnInterval = 3f;
        [SerializeField] float _minSpawnInterval = 1f;
        float _timer, _spawnInterval;

        void Start()
        {
            _spawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
        }

        void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _spawnInterval)
            {
                _timer = 0;
                _spawnInterval = Random.Range(_minSpawnInterval, _maxSpawnInterval);
                var enemy = Instantiate(_enemy, transform);
                enemy.PlayerReference = _playerManager;
            }
        }
    }
}
