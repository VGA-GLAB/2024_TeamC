using UnityEngine;

namespace SoulRunProject.Common
{
    public enum PlayerSkill
    {
        [InspectorName("ソウルバレット")] SoulBullet = 1 << 0 | Projectile,
        [InspectorName("ホーリーフィールド")] HolyField = 1 << 1 | AoE,
        [InspectorName("魂剣")] SoulSword = 1 << 2 | SummoningProjectile,
        [InspectorName("魂の殻")] SoulShell = 1 << 3 | Shield,
        [InspectorName("ソウルレイ")] SoulRay = 1 << 4 | Laser,
        [InspectorName("癒しのソウル")] SoulHealing = 1 << 5 | Healing,
        
        [InspectorName("")] AoE = 1 << 26,
        [InspectorName("")] Healing = 1 << 27,
        [InspectorName("")] Laser = 1 << 28,
        [InspectorName("")] Projectile = 1 << 29,
        [InspectorName("")] Shield = 1 << 30,
        [InspectorName("")] SummoningProjectile = 1 << 31
    }
}