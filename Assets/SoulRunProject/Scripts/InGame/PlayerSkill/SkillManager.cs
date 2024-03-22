using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class SkillManager : MonoBehaviour, IInGameTime
    {
        [SerializeField] List<SkillBase> _skills = new List<SkillBase>();
        
        public void AddSkill(SkillBase skill)
        {
            _skills.Add(skill);
        }
        
        private void StartSkill()
        {
            foreach (var skill in _skills)
            {
                skill.StartSkill();
            }
        }
        
        public void UpdateSkill()
        {
            foreach (var skill in _skills)
            {
                skill.UpdateSkill();
            }
        }
        
        private void StopSkill()
        {
            foreach (var skill in _skills)
            {
                skill.StopSkill();
            }
        }

        public void SwitchPause(bool toPause)
        {
            if (toPause)
            {
                StopSkill();
            }
            else
            {
                StartSkill();
            }
        }
    }
}
