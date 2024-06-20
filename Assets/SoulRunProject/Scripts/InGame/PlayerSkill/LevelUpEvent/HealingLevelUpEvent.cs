using System;
using SoulRunProject.Runtime;
using SoulRunProject.Skill;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// シールドスキルレベルアップイベント抽象クラス
    /// </summary>
    [Serializable, Name("ヒールスキルレベルアップイベントクラス")]
    public class HealingLevelUpEvent : ILevelUpEvent
    {
        enum LevelUpType
        {
            [InspectorName("クールタイム減少")] ReduceCoolTime,
            [InspectorName("一度の回復量を増やす")] IncreaseHealAmount,
        }

        [SerializeField] private LevelUpType _levelUpType;
        
        [SerializeField, CustomLabel("シールドのクールタイムを減少 -% (現在のクールタイムから)"), 
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.ReduceCoolTime)] private float _reduceCoolTime;
        
        [SerializeField, CustomLabel("回復量の増加量"), 
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreaseHealAmount)] private float _increaseHealAmount;
        
        public void LevelUp(in ISkillParameter skillParameter)
        {
            if (skillParameter is HealingSkillParameter param)
            {
                switch (_levelUpType)
                {
                    case LevelUpType.ReduceCoolTime :
                        param.CoolTime *= (100 - _reduceCoolTime) / 100;
                        Debug.Log($"レベルアップで{nameof(HealingSkillData)}のクールタイムを {param.CoolTime}　にアップグレードしました");
                        break;
                    case LevelUpType.IncreaseHealAmount:
                        param.HealAmount += _increaseHealAmount;
                        Debug.Log($"レベルアップで{nameof(HealingSkillData)}の回復量が {param.HealAmount} にアップグレードしました");
                        break;
                }
            }
            else
            {
                Debug.LogError($"{nameof(HealingSkillParameter)}にキャストできませんでした");
            }
        }
    }
}
