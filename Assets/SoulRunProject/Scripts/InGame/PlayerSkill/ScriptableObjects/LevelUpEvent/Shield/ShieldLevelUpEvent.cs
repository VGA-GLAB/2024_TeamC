using System;
using SoulRunProject.Skill;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// シールドスキルレベルアップイベント抽象クラス
    /// </summary>
    [Serializable, Name("シールドスキルレベルアップイベント抽象クラス")]
    public abstract class ShieldLevelUpEvent : ILevelUpEvent
    {
        public void LevelUp(in ISkillParameter skillParameter)
        {
            if (skillParameter is ShieldSkillParameter param)
            {
                LevelUpParam(in param);
            }
            else
            {
                Debug.LogError($"{nameof(ShieldSkillParameter)}にキャストできませんでした");
            }
        }
        public abstract void LevelUpParam(in ShieldSkillParameter param);
    }
    [Serializable, Name("シールドのクールタイムを減少")]
    public class LevelUpEventShieldCoolTime : ShieldLevelUpEvent
    {
        [SerializeField, Header("シールドのクールタイムを減少 -% (現在のクールタイムから)")] private float _reduceCoolTime;
        public override void LevelUpParam(in ShieldSkillParameter param)
        {
            param.CoolTime *= (100 - _reduceCoolTime) / 100;
            Debug.Log($"レベルアップで{nameof(ShieldSkill)}のクールタイムを {param.CoolTime}　にアップグレードしました");
        }
    }
    [Serializable, Name("無効化できる被ダメージ回数を増やす")]
    public class LevelUpEventShieldCount : ShieldLevelUpEvent
    {
        [SerializeField, Header("増加させるシールド数")] private int _increaseShieldCount;
        public override void LevelUpParam(in ShieldSkillParameter param)
        {
            param.ShieldCount += _increaseShieldCount;
            Debug.Log($"レベルアップで{nameof(ShieldSkill)}のダメージ無効化回数を {param.ShieldCount}　にアップグレードしました");
        }
    }
}