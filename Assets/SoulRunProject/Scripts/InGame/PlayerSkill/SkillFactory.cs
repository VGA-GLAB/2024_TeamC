using SoulRunProject.InGame;

namespace SoulRunProject.Common
{
    public static class SkillFactory
    {
        public static AbstractSkill CreateSkill(PlayerSkill skillType, AbstractSkillData skillData, PlayerManager pm)
        {
            if(skillType.HasFlag(PlayerSkill.AoE))
                return new AoESkill(skillData, pm, pm.transform);
            if (skillType.HasFlag(PlayerSkill.Healing))
                return new HealingSkill(skillData, pm, pm.transform);
            if(skillType.HasFlag(PlayerSkill.Projectile))
                return new ProjectileSkill(skillData, pm, pm.transform);
            if(skillType.HasFlag(PlayerSkill.Shield))
                return new ShieldSkill(skillData, pm, pm.transform);
            if(skillType.HasFlag(PlayerSkill.SummoningProjectile))
                return new SummoningProjectileSkill(skillData, pm, pm.transform);
            if (skillType.HasFlag(PlayerSkill.Laser))
                return new LaserSkill(skillData, pm, pm.transform);
            
            return null;
        }
    }
}