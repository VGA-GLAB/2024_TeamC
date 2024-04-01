using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject
{
    /// <summary>
    /// Skillデータを保持するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/SkillDataSet")]
    public class SkillDataSet : ScriptableObject
    {
        [SerializeField, Header("スキルリスト")] private List<SkillData> _skills;
        public List<SkillData> Skills => _skills;
    }
}
