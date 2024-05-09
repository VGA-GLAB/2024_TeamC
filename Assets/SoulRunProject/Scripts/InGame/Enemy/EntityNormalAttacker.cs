using System;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Enemyの通常攻撃処理の実装クラス
    /// </summary>
    [Serializable, Name("通常攻撃")]
    public class EntityNormalAttacker : EntityAttacker
    {
        [SerializeField, CustomLabel("弾速")] float _speed;
        [SerializeField, CustomLabel("弾の寿命")] float _lifeTime;
        [SerializeField, CustomLabel("生成する弾丸")] private EnemyBulletController _enemyBullet;
        private CommonObjectPool _bulletPool;
        ProjectileSkillParameter _parameter;
        float _timer;
        bool _isPause;

        public override void OnStart()
        {
            _bulletPool = ObjectPoolManager.Instance.RequestPool(_enemyBullet);
            _parameter = new ProjectileSkillParameter();
            _timer = _coolTime;
        }

        public override void OnUpdateAttack(Transform myTransform, Transform playerTransform)
        {
            if (_isPause)
            {
                return;
            }

            _parameter.AttackDamage = _attack;
            _parameter.CoolTime = _coolTime;
            _parameter.Range = _range;
            _parameter.Speed = _speed;
            _parameter.LifeTime = _lifeTime;
            if (_parameter.CoolTime > _timer)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _timer = 0;
                var bullet = (EnemyBulletController)_bulletPool.Rent();
                bullet.transform.position = myTransform.position + Vector3.back;
                bullet.ApplyParameter(_parameter);
                bullet.Initialize();
                bullet.OnFinishedAsync.Take(1).Subscribe(_ => _bulletPool.Return(bullet));
            }
        }

        // TODO ポーズ処理怪しい
        public override void Pause()
        {
            _isPause = true;
        }

        public override void Resume()
        {
            _isPause = false;
        }
    }
}