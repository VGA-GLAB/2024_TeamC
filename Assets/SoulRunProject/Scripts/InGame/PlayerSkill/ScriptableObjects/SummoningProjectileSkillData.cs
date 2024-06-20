using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Skill
{
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/SummoningMagicCircleSkill")]
    public class SummoningProjectileSkillData : AbstractSkillData
    {
        [SerializeField, CustomLabel("レベルアップ時のイベントテーブル(パラメーター増加など)")] 
        private LevelUpEventTable<SummoningProjectileLevelUpEvent> _levelUpTable;
        [SerializeField, CustomLabel("魔法陣の列数")] private int _count;
        [SerializeField, CustomLabel("発射する剣のプレハブ")] private PlayerBullet _sword;
        [SerializeField, CustomLabel("剣を召喚する魔法陣のプレハブ")] private GameObject _magicCirclePrefab;
        [SerializeField] private Vector3 _muzzleOffset;
        [SerializeField, CustomLabel("魔法陣起動から射出までの遅延")] private float _swordShotDelay;
        
        private SummoningProjectileSkillData()
        {
            _skillParameter = new ProjectileSkillParameter();
        }
        public override List<List<ILevelUpEvent>> LevelUpTable =>
            _levelUpTable.List.Select(list => list.List.OfType<ILevelUpEvent>().ToList()).ToList();
        
        public ProjectileSkillParameter Parameter => 
            (ProjectileSkillParameter)_skillParameter;

        public GameObject MagicCirclePrefab => _magicCirclePrefab;
        public Vector3 MuzzleOffset => _muzzleOffset;
        public float SwordShotDelay => _swordShotDelay;
        public PlayerBullet Sword => _sword;
        public int Count => _count;
    }
}