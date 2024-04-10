using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject
{
    /// <summary>
    /// Skillデータを保持するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/SkillData")]
    public class SkillData : ScriptableObject
    {
        [SerializeField, Header("スキルリスト")] private List<SkillBase> _skills;
        public List<SkillBase> Skills => _skills;
    }
}
