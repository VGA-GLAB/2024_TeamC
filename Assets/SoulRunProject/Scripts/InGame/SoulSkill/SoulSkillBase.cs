using System;
using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.Framework;
using UniRx;
using UnityEngine;

namespace SoulRunProject
{
    /// <summary>
    /// ソウル技のインターフェース
    /// </summary>
    [Serializable]
    public abstract class SoulSkillBase : MonoBehaviour
    {
        [SerializeField] protected SkillParameter _skillParameter;
        [SerializeField] private float _requiredSoul;
        private FloatReactiveProperty _currentSoul = new FloatReactiveProperty(0);
        public IObservable<float> OnCurrentSoulChanged => _currentSoul;
        public float MaxSoul => _requiredSoul;
        
        public void AddSoul(float soul)
        {
            _currentSoul.Value += soul;
            if (_currentSoul.Value >= _requiredSoul)
            {
                _currentSoul.Value = _requiredSoul;
            }
        }

        public void UseSoulSkill()
        { 
            DebugClass.Instance.ShowLog($"現在のソウル値：{_currentSoul.Value}/必要ソウル値：{_requiredSoul}");
            if (_currentSoul.Value < _requiredSoul)
            {
                return;
            }
            _currentSoul.Value -= _requiredSoul;
            StartSoulSkill();
        }
        
        protected abstract void StartSoulSkill();
    }
}
