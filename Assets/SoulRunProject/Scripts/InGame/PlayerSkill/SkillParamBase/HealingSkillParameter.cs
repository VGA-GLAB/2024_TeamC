using System;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class HealingSkillParameter : ISkillParameter
    {
        [SerializeField, CustomLabel("クールタイム")] private float _coolTime;
        [SerializeField, CustomLabel("一度の回復量")] private float _healAmount;
        private PlayerStatus _status;
        public float CoolTime
        {
            get => _coolTime;
            set => _coolTime = value;
        }
        public float HealAmount
        {
            get => _healAmount;
            set => _healAmount = value;
        }
        public void SetPlayerStatus(in PlayerStatus status)
        {
            _status = status;
        }

        public ISkillParameter Clone()
        {
            return new HealingSkillParameter(this);
        }

        HealingSkillParameter(HealingSkillParameter param)
        {
            _coolTime = param._coolTime;
            _healAmount = param._healAmount;
            _status = param._status;
        }
        public HealingSkillParameter(){}
    }
}
