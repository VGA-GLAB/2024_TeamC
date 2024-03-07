using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.InGame.PlayerSkill
{
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/LevelUpTable")]
    public class LevelUpTable : ScriptableObject
    {
        [SerializeField] List<SkillParameter> _skillParameters;
        public List<SkillParameter> SkillParameters => _skillParameters;
    }
}