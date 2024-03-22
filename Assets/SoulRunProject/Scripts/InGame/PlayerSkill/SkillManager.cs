using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class SkillManager : MonoBehaviour
    {
        [SerializeField] List<SkillBase> _skills = new List<SkillBase>();
        
        public void AddSkill(SkillBase skill)
        {
            _skills.Add(skill);
        }
        
        public void StartSkill()
        {
            foreach (var skill in _skills)
            {
                skill.Start();
            }
        }
        
        public void UpdateSkill()
        {
            foreach (var skill in _skills)
            {
                skill.Update();
            }
        }
        
        public void StopSkill()
        {
            foreach (var skill in _skills)
            {
                skill.Stop();
            }
        }
    }
}
