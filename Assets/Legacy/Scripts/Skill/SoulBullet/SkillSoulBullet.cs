using UnityEngine;
using Cysharp.Threading.Tasks;
using UniRx;
using System;
using VContainer;
using Unity.VisualScripting;

namespace SoulRun.InGame
{
    /// <summary>
    /// 前方にソウル弾を発射するスキル
    /// 弾丸オブジェクトを指定したクールダウンで生成し、前に飛ばす
    /// </summary>
    public class SkillSoulBullet : SkillBase
    {
        [SerializeField] private GameObject _soulObject;
        private float _bulletAliveTime = 0.5f;
        [Inject] private PlayerManager _playerManager;
        [Inject] private InGameManager _InGameManager;
        private SoulBulletFactory _factory;
        private float _bulletSpeed = 35;
        private Vector3 _instantiateOffset = new(0, 0, 1);
        private IDisposable _currentShootRoutine;

        private void Start()
        {
            var objPool = this.AddComponent<GameObjectPoolForUnity>();
            _factory = new SoulBulletFactory(objPool, _soulObject, _InGameManager);
        }

        public override void ActivateCurrentStatusSkill()
        {
            _currentShootRoutine = Observable.Interval(TimeSpan.FromSeconds(_coolDownTime))
                .Subscribe(_ => ShootSoulBullet()).AddTo(this);
        }

        public override void DeactivateCurrentStatusSkill()
        {
            _currentShootRoutine?.Dispose();
            _currentShootRoutine = null;
        }

        /// <summary>
        /// ソウルオブジェクトを前方に放つ
        /// </summary>
        private void ShootSoulBullet()
        {
            if (!_playerManager) return;
            var bullet = _factory.Create();
            bullet.transform.position = _playerManager.transform.position + _instantiateOffset;
            bullet.GetComponent<SoulBulletObject>().SetBulletStatus(_bulletSpeed, _bulletAliveTime);
        }
    }
}
