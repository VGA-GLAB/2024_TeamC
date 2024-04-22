using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    public class HealingSkillParameter : ISkillParameter
    {
        [SerializeField, CustomLabel("クールタイム")] private float _coolTime;
        [SerializeField, CustomLabel("一度の回復量")] private float _healAmount;

        [NonSerialized] public float CoolTime;
        [NonSerialized] public float HealAmount;
        
        public void InitializeParamOnSceneLoaded()
        {
            CoolTime = _coolTime;
            HealAmount = _healAmount;
        }
    }
}
