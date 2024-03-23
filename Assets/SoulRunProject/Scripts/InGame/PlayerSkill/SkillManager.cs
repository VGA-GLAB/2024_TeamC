using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class SkillManager : MonoBehaviour, IInGameTime
    {
        [SerializeField , Header("スキルデータセット")] private SkillDataSet _skillDataSet;
        private SkillDataSet _skillData;
        private readonly List<ISkill> _currentSkills = new(5);
        private bool _isPause;
        public void Start()
        {
            //Instantiateしないと、ScriptableObject内のクラスが生成されない。
            _skillData = Instantiate(_skillDataSet);
            AddSkill(_skillData.Skills[0]);
        }

        public void AddSkill(ISkill skill)
        {
            _currentSkills.Add(skill);
        }
        
        //TODO とりあえずUpdateで動かしているが
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
        
        public void SwitchPause(bool toPause)
        {
            _isPause = toPause;
        }
    }
}
