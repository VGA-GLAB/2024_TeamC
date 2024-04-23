using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using SoulRunProject.Common;
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
        [SerializeField, CustomLabel("名前")] private string _name;
        [SerializeField, CustomLabel("技の説明文")] private string _description;
        [SerializeField, CustomLabel("発動に必要なソウル")] private float _requiredSoul;
        public string Name => _name;

        public string Description => _description;

        public float RequiredSoul => _requiredSoul;
        
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
