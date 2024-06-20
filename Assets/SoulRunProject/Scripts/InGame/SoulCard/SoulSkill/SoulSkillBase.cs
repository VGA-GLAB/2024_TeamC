using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;
using UniRx;
using UnityEngine;

namespace SoulRunProject
{
    /// <summary>
    /// ソウル技のインターフェース
    /// </summary>
    [Serializable]
    public abstract class SoulSkillBase : ScriptableObject
    {
        [SerializeField, CustomLabel("ソウル技のタイプ")] private SoulSkillType _skillType;
        [SerializeField, CustomLabel("名前")] private string _name;
        [SerializeField, CustomLabel("技の説明文")] private string _description;
        [SerializeField, CustomLabel("発動に必要なソウル")] private float _requiredSoul;
        [SerializeField, CustomLabel("発動時間")] protected int _duration;
        public SoulSkillType SkillType => _skillType;
        
        public string Name => _name;

        public string Description => _description;

        public float RequiredSoul => _requiredSoul;

        public float Duration => _duration;
        
        /// <summary>
        /// ソウル技を実行する
        /// </summary>
        public abstract void StartSoulSkill();
        
        /// <summary>
        /// ソウル技を実行する
        /// </summary>
        public abstract void UpdateSoulSkill(float deltaTime);
        
        /// <summary>
        /// ソウル技を一時停止する
        /// </summary>
        public abstract void PauseSoulSkill(bool isPause);
    }
}
