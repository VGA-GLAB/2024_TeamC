using UnityEngine;
using System.Collections;
using VContainer;

namespace SoulRun.InGame
{
    /// <summary>
    /// 一定間隔でプレイヤーに向かって弾を生成する
    /// </summary>
    public class EnemyAttackShoot : EnemyAttackRoutineBase
    {
        [SerializeField] private GameObject _bulletPrefab; // 弾のプレハブ
        [SerializeField] private float _shootingInterval = 2f; // 弾を発射する間隔
        private WaitForSeconds _cachedWaitForSecond;
        [Inject] private Transform _player;
        private float _enableShotOffset = 5;    //Playerの位置からこの値足したZ座標を下回ったら非アクティブに

        private void Start()
        {
            _cachedWaitForSecond = new WaitForSeconds(_shootingInterval);
        }

        public override void ActivateAttackRoutine()
        {
            if (_bulletPrefab == null) return;
            StopAllCoroutines();
            if (isActiveAndEnabled) StartCoroutine(ShootingRoutine());
        }

        public override void DeactivateAttackRoutine()
        {
            StopAllCoroutines();
        }

        private IEnumerator ShootingRoutine()
        {
            while (true)
            {
                if (_player == null || transform.position.z < _player.position.z + _enableShotOffset)
                {
                    DeactivateAttackRoutine();
                    break;
                }
                Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
                yield return _cachedWaitForSecond;
            }
        }
    }
}
