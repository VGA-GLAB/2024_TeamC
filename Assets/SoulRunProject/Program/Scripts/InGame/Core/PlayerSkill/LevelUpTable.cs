using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.InGame.PlayerSkill
{
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/LevelUpTable")]
    public class LevelUpTable : ScriptableObject
    {
        [field: SerializeField] public List<SkillParameter> SkillParameters { get; private set; }
    }
}
