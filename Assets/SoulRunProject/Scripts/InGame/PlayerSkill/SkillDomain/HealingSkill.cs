using SoulRunProject.Common;
using SoulRunProject.Skill;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class HealingSkill : AbstractSkill
    {
        private float _coolTimer;
        private ParticleSystem _ps;

        public HealingSkill(AbstractSkillData skillData, in PlayerManager playerManager,
            in Transform playerTransform)
            : base(skillData, in playerManager, in playerTransform)
        {
        }

        private HealingSkillData SkillData => (HealingSkillData)_skillData;
        private HealingSkillParameter RuntimeParameter => (HealingSkillParameter)_runtimeParameter;

        public override void StartSkill()
        {
            _ps = Object.Instantiate(SkillData.Original, _playerTransform);
            ActivateSkill();
        }

        public override void UpdateSkill(float deltaTime)
        {
            if (_coolTimer > 0)
                _coolTimer -= deltaTime;
            else if (_playerManagerInstance.CurrentPlayerStatus.CurrentHp <
                     _playerManagerInstance.CurrentPlayerStatus.MaxHp) // HPがMaxだと発動しない
                ActivateSkill();
        }

        public override void OnLevelUp()
        {
        }

        /// <summary> スキル発動 </summary>
        private void ActivateSkill()
        {
            CriAudioManager.Instance.PlaySE("SE_Heal");
            _coolTimer = RuntimeParameter.CoolTime;
            _playerManagerInstance.Heal(RuntimeParameter.HealAmount);
            _ps?.Play();
        }
    }
}