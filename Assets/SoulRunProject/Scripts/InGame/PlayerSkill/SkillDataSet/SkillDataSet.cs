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
        [SerializeReference, SubclassSelector, Header("スキルリスト")]  List<ISkill> _skills;

        public List<ISkill> Skills => _skills;
    }
}
