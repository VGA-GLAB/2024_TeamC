using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.Skill
{
    /// <summary>
    ///     一定時間間隔で回復
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/HealingSkill")]
    public class HealingSkillData : AbstractSkillData
    {
        //  メンバー変数
        [SerializeField, CustomLabel("レベルアップ時のイベントテーブル(パラメーター増加など)")] private LevelUpEventTable<HealingLevelUpEvent> _levelUpTable;
        [SerializeField] [Tooltip("スキルエフェクト")] private ParticleSystem _healParticle;
        //  コンストラクター
        private HealingSkillData()
        {
            _skillParameter = new HealingSkillParameter();
        }
        //  プロパティ
        public override List<List<ILevelUpEvent>> LevelUpTable =>
            _levelUpTable.List.Select(list => list.List.OfType<ILevelUpEvent>().ToList()).ToList();

        public HealingSkillParameter Parameter => (HealingSkillParameter)_skillParameter;
        public ParticleSystem Original => _healParticle;
    }
}