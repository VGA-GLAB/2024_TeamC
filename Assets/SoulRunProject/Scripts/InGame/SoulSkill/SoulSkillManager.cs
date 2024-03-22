using System;
using SoulRunProject.Framework;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ソウル技を管理するクラス
    /// </summary>
    public class SoulSkillManager : MonoBehaviour
    {
        [SerializeReference] SoulSkillBase _currentSoulSkill;
        [SerializeField] private FloatReactiveProperty _currentSoul = new FloatReactiveProperty(0);
        public float RequiredSoul => _currentSoulSkill.RequiredSoul;
        public IObservable<float> CurrentSoul => _currentSoul;
        public void SetSoulSkill(SoulSkillBase soulSkill)
        {
            _currentSoulSkill = soulSkill;
        }
        
        public void AddSoul(float soul)
        {
            _currentSoul.Value += soul;
            if (_currentSoul.Value >= _currentSoulSkill.RequiredSoul)
            {
                _currentSoul.Value = _currentSoulSkill.RequiredSoul;
            }
        }
        
        public void UseSoulSkill()
        { 
            DebugClass.Instance.ShowLog($"現在のソウル値：{_currentSoul.Value}/必要ソウル値：{RequiredSoul}");
            if (_currentSoul.Value < RequiredSoul)
            {
                return;
            }
            _currentSoul.Value -= RequiredSoul;
            _currentSoulSkill.StartSoulSkill();
        }
    }
}
