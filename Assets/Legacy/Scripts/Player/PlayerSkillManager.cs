using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VContainer;

namespace SoulRun.InGame
{
    /// <summary>
    /// プレイヤーのスキルを管理するクラス
    /// 追加、削除、使用処理を持つ
    /// </summary>
    public class PlayerSkillManager
    {
        HashSet<SkillBase> _obtainSkills = new();

        [Inject]
        public PlayerSkillManager(List<SkillBase> skills) 
        {
            _obtainSkills = skills.ToHashSet();
        }

        /// <summary>
        /// 所持している全スキルを発動する
        /// </summary>
        public void ActivateAllSkills()
        {
            foreach (var skill in _obtainSkills)
            {
                skill.ActivateCurrentStatusSkill();
            }
        }

        /// <summary>
        /// 所持している全スキルを止める
        /// </summary>
        public void DeactiveAllSkills()
        {
            foreach (var skill in _obtainSkills)
            {
                skill.DeactivateCurrentStatusSkill();
            }
        }

        public void AddSkill(SkillBase skill)
        {
            _obtainSkills.Add(skill);
        }

        public void RemoveSkill(SkillBase skill)
        {
            _obtainSkills.Remove(skill);
        }
    }
}
