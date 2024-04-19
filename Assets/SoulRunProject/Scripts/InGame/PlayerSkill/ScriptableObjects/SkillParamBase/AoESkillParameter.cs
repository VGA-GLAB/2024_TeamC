using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable , Name("範囲スキルパラメータ")]
    public class AoESkillParameter : ISkillParameter
    {
        [SerializeField, CustomLabelAttribute("敵にヒットしたときに与えるダメージ")] float _attackDamage;
        [SerializeField, CustomLabelAttribute("スキルのオブジェクトの大きさ")] float _range;
        
        [NonSerialized] public float AttackDamage;
        [NonSerialized] public float Range;
        
        public void InitializeParamOnSceneLoaded()
        {
            AttackDamage = _attackDamage;
            Range = _range;
        }
    }
}