using System;
using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject
{
    /// <summary>
    /// ソウル技のインターフェース
    /// </summary>
    public abstract class SoulSkillBase : MonoBehaviour
    {
        [SerializeField] private SkillParameterBase _skillParameterBase;
        [SerializeField] private float _requiredSoul;
        private float _currentSoul = 0;
        
        public void AddSoul(float soul)
        {
            _currentSoul += soul;
            if (_currentSoul >= _requiredSoul)
            {
                _currentSoul = _requiredSoul;
            }
        }

        public void UseSoulSkill()
        {
            if (_currentSoul < _requiredSoul)
            {
                return;
            }
            _currentSoul -= _requiredSoul;
            StartSoulSkill();
        }
        
        protected abstract void StartSoulSkill();
    }
}
