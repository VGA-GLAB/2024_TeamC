using System;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ソウル技を管理するクラス
    /// </summary>
    public class SoulSkillManager : MonoBehaviour
    {
        [SerializeField] SoulSkillBase _currentSoulSkill;
        public float MaxSoul => _currentSoulSkill.MaxSoul;
        public IObservable<float> CurrentSoul => _currentSoulSkill.OnCurrentSoulChanged;
        
        public void SetSoulSkill(SoulSkillBase soulSkill)
        {
            _currentSoulSkill = soulSkill;
        }
        
        public void AddSoul(float soul)
        {
            _currentSoulSkill.AddSoul(soul);
        }
        
        public void UseSoulSkill()
        {
            _currentSoulSkill.UseSoulSkill();
        }
    }
}
