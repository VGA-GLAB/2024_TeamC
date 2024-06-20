using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class PlayerBullet : BulletBase
    {
        float _attackDamage;
        float _range;
        float _speed;
        int _penetration;
        int _hitCount;
        private GiveKnockBack _giveKnockBack;
        private float _getSoulValueOnHit;
        public void ApplyParameter(ProjectileSkillParameter param)
        {
            _lifeTime = param.LifeTime;
            _attackDamage = param.AttackDamage;
            _range = param.Size;
            _speed = param.Speed;
            _penetration = param.Penetration;
            _giveKnockBack = param.KnockBack;
            _getSoulValueOnHit = param.GetSoulValueOnHit;
        }

        public override void Initialize()
        {
            transform.localScale = new Vector3(_range, _range, _range);
            base.Initialize();
        }

        public override void Move()
        {
            if (_isPause) return;
            transform.position += transform.forward * (_speed * Time.fixedDeltaTime);
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out DamageableEntity entity))
            {
                entity.Damage(_attackDamage , in _giveKnockBack);
                if (other.gameObject.TryGetComponent(out EnemyController _)) PlayerManagerInstance.AddSoul(_getSoulValueOnHit);
                _hitCount++;
                if (_hitCount > _penetration)
                {
                    OnHit(other);
                }
            }
        }
    }
}