using System;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class ShieldSkillParameter : ISkillParameter
    {
        [SerializeField, CustomLabel("次にこのスキルを使えるまでの時間")] float _coolTime;
        [SerializeField, CustomLabel("ダメージ無効化回数")] int _shieldCount;
        private PlayerStatus _status;
        public float BaseCoolTime { get => _coolTime; set => _coolTime = value; }
        public int BaseShieldCount { get => _shieldCount; set => _shieldCount = value; }
        public float CoolTime => BaseCoolTime * _status.CoolTimeReductionRate;
        public float ShieldCount => BaseShieldCount;
        public void SetPlayerStatus(in PlayerStatus status)
        {
            _status = status;
        }

        public ISkillParameter Clone()
        {
            return new ShieldSkillParameter(this);
        }

        ShieldSkillParameter(ShieldSkillParameter param)
        {
            _coolTime = param._coolTime;
            _shieldCount = param._shieldCount;
            _status = param._status;
        }
        public ShieldSkillParameter(){}
    }
}