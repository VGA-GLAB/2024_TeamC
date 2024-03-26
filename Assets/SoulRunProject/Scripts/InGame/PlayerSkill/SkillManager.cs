using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class SkillManager : MonoBehaviour, IInGameTime
    {
        [SerializeField , Header("スキルデータセット")] private SkillDataSet _skillDataSet;
        private SkillDataSet _skillData;
        private readonly List<SkillBase> _currentSkills = new(5);
        private bool _isPause;
        public void Start()
        {
            //Instantiateしないと、ScriptableObject内のクラスが生成されない。
            _skillData = Instantiate(_skillDataSet);
            AddSkill(PlayerSkill.SoulBullet);
            foreach (var skill in _currentSkills)
            {
                skill.StartSkill();
            }
        }
        
        public void Update()
        {
            if (!_isPause)
            {
                foreach (var skill in _currentSkills)
                {
                    skill.UpdateSkill(Time.deltaTime);
                }
            }
        }
        
        public void AddSkill(PlayerSkill skillType)
        {
            var skillBase = _skillData.Skills.FirstOrDefault(x => x.SkillType == skillType);
            if (skillBase != null)
            {
                _currentSkills.Add(skillBase);
            }
            else
            {
                Debug.LogError("スキルリストに入っていないスキルがレベルアップ選択されました。");
            }
        }

        public void LevelUpSkill(PlayerSkill skillType)
        {
            var skill = _currentSkills.FirstOrDefault(x => x.SkillType == skillType);
            if (skill != null)
            {
                skill.LevelUp();
            }
            else
            {
                Debug.LogError("スキルリストに入っていないスキルがレベルアップ選択されました。");
            }
        }
        
        public void SwitchPause(bool toPause)
        {
            _isPause = toPause;
        }
    }
}
