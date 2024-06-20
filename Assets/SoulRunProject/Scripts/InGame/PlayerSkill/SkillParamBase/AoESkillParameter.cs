using System;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class AoESkillParameter : ISkillParameter
    {
        [SerializeField, CustomLabel("敵にヒットしたときに与えるダメージ")] float _attackDamage;
        [SerializeField, CustomLabel("スキルのオブジェクトの大きさ")] float _size;
        [SerializeField, CustomLabel("スキルを当てたときのソウルの増える量(秒)")] private float _getSoulPerSec;
        private PlayerStatus _status;
        public float BaseAttackDamage
        {
            get => _attackDamage;
            set => _attackDamage = value;
        }
        public float BaseSize
        {
            get => _size;
            set => _size = value;
        }
        public float GetSoulPerSec => _getSoulPerSec;
        public float AttackDamage => _attackDamage + _status.AttackValue;
        public float Size => _size * _status.SkillSizeUpRate;
        
        public void SetPlayerStatus(in PlayerStatus status)
        {
            _status = status;
        }

        public ISkillParameter Clone()
        {
            return new AoESkillParameter(this);
        }

        AoESkillParameter(AoESkillParameter param)
        {
            _attackDamage = param._attackDamage;
            _size = param._size;
            _getSoulPerSec = param._getSoulPerSec;
            _status = param._status;
        }
        public AoESkillParameter(){}
    }
}