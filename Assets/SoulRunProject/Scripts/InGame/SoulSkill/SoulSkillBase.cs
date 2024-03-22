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
        public float RequiredSoul => _requiredSoul;
        public abstract void StartSoulSkill();
    }
}
