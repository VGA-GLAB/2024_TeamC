using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class ProjectileSkill : AbstractSkill
    {
        private CommonObjectPool _bulletPool;
        private float _timer;

        public ProjectileSkill(AbstractSkillData skillData, in PlayerManager playerManager,
            in Transform playerTransform) : base(skillData, in playerManager, in playerTransform)
        {
        }

        private ProjectileSkillData SkillData => (ProjectileSkillData)_skillData;
        private ProjectileSkillParameter RuntimeParameter => (ProjectileSkillParameter)_runtimeParameter;

        public override void StartSkill()
        {
            _timer = 0f;
            _bulletPool = ObjectPoolManager.Instance.RequestPool(SkillData.Bullet);
        }

        public override void UpdateSkill(float deltaTime)
        {
            var currentCoolTime = RuntimeParameter.CoolTime;
            if (_timer < currentCoolTime)
            {
                _timer += deltaTime;
            }
            else
            {
                _timer = 0;
                if (_runtimeParameter != null)
                {
                    for (var i = 0; i < RuntimeParameter.Amount; i++)
                    {
                        // 弾の生成
                        float rotateY;
                        if (RuntimeParameter.Amount % 2 == 0)
                            rotateY = (i - RuntimeParameter.Amount / 2f) *
                                      SkillData.BaseRotateY +
                                      SkillData.BaseRotateY / 2;
                        else
                            rotateY = RuntimeParameter.Amount / 2 * -SkillData.BaseRotateY + i * SkillData.BaseRotateY;

                        var bullet = (PlayerBullet)_bulletPool.Rent();
                        bullet.transform.position = _playerTransform.position + SkillData.MuzzleOffset;
                        bullet.transform.forward = _playerTransform.forward;
                        bullet.transform.rotation *= Quaternion.Euler(0f, rotateY, 0f);
                        bullet.ApplyParameter(RuntimeParameter);
                        bullet.Initialize();
                        bullet.GetReference(_playerManagerInstance);
                        bullet.OnFinishedAsync.Take(1).Subscribe(_ => _bulletPool.Return(bullet));
                    }

                    CriAudioManager.Instance.PlaySE("SE_Soulbullet");
                }
            }
        }

        public override void OnLevelUp()
        {
        }
    }
}