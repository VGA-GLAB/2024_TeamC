using System;
using System.Collections.Generic;
using SoulRunProject.Common;
using SoulRunProject.Framework;
using SoulRunProject.SoulMixScene;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ソウル技を管理するクラス
    /// </summary>
    public class SoulSkillManager : MonoBehaviour
    {
        [SerializeField] private float _initialSoul;
        [SerializeField] private float _regenerativePerSec;
        
        private FloatReactiveProperty _currentSoul = new FloatReactiveProperty(0);
        private readonly Dictionary<SoulSkillType , SoulSkillBase> _soulSkillReference = new();
        SoulSkillBase _currentSoulSkill;
        private float _usingSkillTimer;
        public float RequiredSoul { get; private set; }
        public IObservable<float> CurrentSoul => _currentSoul;

        private void Start()
        {
            _currentSoul.AddTo(this);
            
            //TODO デバック用　ソウルフレイム設定。
            if (MyRepository.Instance.TryGetDataList<SoulSkillBase>(out var dataList))
            {
                foreach (var soulSkill in dataList)
                {
                    _soulSkillReference.Add(soulSkill.SkillType , soulSkill);
                }
            }

            SetSoulSkill(SoulSkillType.SoulFrame);
            AddSoul(_initialSoul);
        }

        private void FixedUpdate()
        {
            if (_usingSkillTimer > 0)
            {
                _usingSkillTimer -= Time.fixedDeltaTime;
                _currentSoul.Value -= RequiredSoul / _currentSoulSkill.Duration * Time.fixedDeltaTime;
            }
            
            AddSoul(_regenerativePerSec * Time.fixedDeltaTime);
        }

        public void SetSoulSkill(SoulSkillType soulSkillType)
        {
            _currentSoulSkill = _soulSkillReference[soulSkillType];
            RequiredSoul = _currentSoulSkill.RequiredSoul;
        }
        
        public void AddSoul(float soul)
        {
            if (_usingSkillTimer > 0) return;
            
            if (_currentSoul.Value >= RequiredSoul)
            {
                _currentSoul.Value = RequiredSoul;
                return;
            }
            
            _currentSoul.Value += soul;
            
            if (_currentSoul.Value >= RequiredSoul)
            {
                _currentSoul.Value = RequiredSoul;
                CriAudioManager.Instance.PlaySE("SE_SoulSkillAvailable");
            }
        }
        
        public void UseSoulSkill()
        { 
            DebugClass.Instance.ShowLog($"現在のソウル値：{_currentSoul.Value}/必要ソウル値：{RequiredSoul}");
            //TODO 処理の前後をずらしてテストように呼び出せるようにしている
            if (_currentSoul.Value < RequiredSoul && _usingSkillTimer <= 0)
            {
                Debug.Log("ソウルが足りません。");
                return;
            }
            _currentSoulSkill.StartSoulSkill();
            _usingSkillTimer = _currentSoulSkill.Duration;
        }
    }
}
