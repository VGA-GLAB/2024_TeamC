using System;
using SoulRunProject.Runtime;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("飛翔物召喚スキルレベルアップイベント")]
    public class SummoningProjectileLevelUpEvent : ILevelUpEvent
    {
        enum LevelUpType
        {
            [InspectorName("ダメージ増加")] IncreaseDamage,
            [InspectorName("召喚列数増加")] IncreaseSummonLineCount,
            [InspectorName("貫通力増加")] IncreasePenetrationPower,
            [InspectorName("召喚クールタイムを減少")] ReduceCoolTime
        }

        [SerializeField] private LevelUpType _levelUpType;

        [SerializeField, CustomLabel("ダメージ増加量"), ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreaseDamage)]
        private float _increaseDamageValue;

        [SerializeField, CustomLabel("魔法陣生成列数増加量"),
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreaseSummonLineCount)]
        private int _increaseSummonLineValue;

        [SerializeField, CustomLabel("貫通力増加量"),
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreasePenetrationPower)]
        private int _increasePenetrationPower;

        [SerializeField, CustomLabel("召喚のクールタイムを減少 -% (現在のクールタイムから)"),
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.ReduceCoolTime)]
        private float _reduceCoolTime;

        public void LevelUp(in ISkillParameter skillParameter)
        {
            if (skillParameter is ProjectileSkillParameter param)
            {
                switch (_levelUpType)
                {
                    case LevelUpType.IncreaseDamage:
                        param.BaseAttackDamage += _increaseDamageValue;
                        break;
                    case LevelUpType.IncreaseSummonLineCount:
                        param.BaseAmount += _increaseSummonLineValue;
                        break;
                    case LevelUpType.IncreasePenetrationPower:
                        param.BasePenetration += _increasePenetrationPower;
                        break;
                    case LevelUpType.ReduceCoolTime:
                        param.BaseCoolTime *= (100 - _reduceCoolTime) / 100;
                        break;
                }
            }
            else
            {
                Debug.LogError($"{nameof(ProjectileSkillParameter)}にキャストできませんでした");
            }
        }
    }
}