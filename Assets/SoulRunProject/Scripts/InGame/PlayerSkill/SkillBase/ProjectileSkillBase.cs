using System;
using SoulRunProject.Common;
using SoulRunProject.InGame;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SoulRunProject
{
    /// <summary>
    /// 表現を加える際にはさらにこれを継承する
    /// </summary>
    [Serializable]
    public class ProjectileSkillBase : SkillBase
    {
        [SerializeField , Header("発射する弾のプレハブ")] private BulletController _bullet;
        [SerializeField] Vector3 _muzzleOffset;
        private Transform _playerTransform;
        float _playerMoveSpeed;
        BulletPool _bulletPool;
        public BulletController Bullet => _bullet;

        public override void StartSkill()
        {
            _bulletPool = BulletPoolManager.Instance.Get(SkillType);
        }

        public override void Fire()
        {
            //プレイヤーのポジションから発射させたい
            _playerTransform ??= Object.FindObjectOfType<PlayerManager>().transform;
            var playerForwardMover = Object.FindObjectOfType<PlayerForwardMover>();
            if (playerForwardMover)
            {
                _playerMoveSpeed = playerForwardMover.Speed;
            }
            if (SkillBaseParam is ProjectileSkillParameter param)
            {
                // 弾の生成
                var bullet = _bulletPool.Rent();
                bullet.transform.position = _playerTransform.position + _muzzleOffset;
                bullet.Initialize(param.LifeTime , param.AttackDamage , param.Range , param.Speed + _playerMoveSpeed , param.Penetration);
                bullet.OnFinishedAsync.Take(1)
                    .Subscribe(_ =>
                    {
                        _bulletPool.Return(bullet);
                    });
            }
            else
            {
                Debug.LogError("パラメータが　ProjectionSkillParameter　ではありません　");
            }
        }
    }
}
