using SoulRunProject.Common;
using SoulRunProject.InGame;
using UniRx;
using UnityEngine;

namespace SoulRunProject
{
    /// <summary>
    /// 飛翔物発射スキル
    /// <br/>表現を加える際にはさらにこれを継承する
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/ProjectileSkill")]
    public class ProjectileSkill : SkillBase
    {
        static Transform _playerTransform;
        static PlayerForwardMover _playerForwardMover;
        [SerializeField, Header("発射する弾のプレハブ")] BulletController _bullet;
        [SerializeField] Vector3 _muzzleOffset;
        CommonObjectPool _bulletPool;
        ProjectileSkillParameter _projectileSkillParameter;
        float _currentCoolTime;

        ProjectileSkill()
        {
            _skillParam = new ProjectileSkillParameter();
            SkillLevelUpEvent = new SkillLevelUpEvent(new ProjectileSkillLevelUpEventListList());
        }

        public BulletController Bullet => _bullet;

        public override void InitializeParamOnSceneLoaded()
        {
            base.InitializeParamOnSceneLoaded();
            _currentCoolTime = 0f;
        }

        public override void StartSkill()
        {
            _bulletPool = ObjectPoolManager.Instance.RequestPool(_bullet);
            _playerTransform = FindObjectOfType<PlayerManager>().transform;
            _playerForwardMover = FindObjectOfType<PlayerForwardMover>();
            if (_skillParam is ProjectileSkillParameter param)
                _projectileSkillParameter = param;
            else
                Debug.LogError($"パラメータが　{nameof(ProjectileSkillParameter)}　ではありません　");
        }

        public override void UpdateSkill(float deltaTime)
        {
            if (_currentCoolTime < _projectileSkillParameter.CoolTime)
            {
                _currentCoolTime += deltaTime;
            }
            else
            {
                _currentCoolTime = 0;
                if (_projectileSkillParameter != null)
                {
                    // 弾の生成
                    var bullet = (BulletController)_bulletPool.Rent();
                    bullet.transform.position = _playerTransform.position + _muzzleOffset;
                    bullet.transform.forward = _playerTransform.forward;
                    bullet.ApplyParameter(_projectileSkillParameter);
                    bullet.Initialize();
                    bullet.OnFinishedAsync.Take(1).Subscribe(_ => _bulletPool.Return(bullet));
                    CriAudioManager.Instance.PlaySE(CriAudioManager.CueSheet.Se, "SE_Soulbullet");
                }
            }
        }
    }
}