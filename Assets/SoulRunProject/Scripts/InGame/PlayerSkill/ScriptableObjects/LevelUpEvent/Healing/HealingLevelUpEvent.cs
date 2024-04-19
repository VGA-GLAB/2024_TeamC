using System;
using SoulRunProject.Skill;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// シールドスキルレベルアップイベント抽象クラス
    /// </summary>
    [Serializable, Name("ヒールスキルレベルアップイベント抽象クラス")]
    public abstract class HealingLevelUpEvent : ILevelUpEvent
    {
        public void LevelUp(in ISkillParameter skillParameter)
        {
            if (skillParameter is HealingSkillParameter param)
            {
                LevelUpParam(in param);
            }
            else
            {
                Debug.LogError($"{nameof(HealingSkillParameter)}にキャストできませんでした");
            }
        }
        public abstract void LevelUpParam(in HealingSkillParameter param);
    }

    [Serializable, Name("クールタイムを減少")]
    public class LevelUpEventHealingCoolTime : HealingLevelUpEvent
    {
        [SerializeField, Header("シールドのクールタイムを減少 -% (現在のクールタイムから)")] private float _reduceCoolTime;
        
        public override void LevelUpParam(in HealingSkillParameter param)
        {
            param.CoolTime *= (100 - _reduceCoolTime) / 100;
            Debug.Log($"レベルアップで{nameof(HealingSkill)}のクールタイムを {param.CoolTime}　にアップグレードしました");
        }
    }

    [Serializable, Name("一度の回復量を増やす")]
    public class LevelUpEventIncreaseHealAmount : HealingLevelUpEvent
    {
        [SerializeField, Header("回復量の増加量")] private float _increaseHealAmount;
        
        public override void LevelUpParam(in HealingSkillParameter param)
        {
            param.HealAmount += _increaseHealAmount;
            Debug.Log($"レベルアップで{nameof(HealingSkill)}の回復量が {param.HealAmount} にアップグレードしました");
        }
    }
}
