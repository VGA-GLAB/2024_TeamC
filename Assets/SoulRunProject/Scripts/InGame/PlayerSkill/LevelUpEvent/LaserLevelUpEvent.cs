using System;
using SoulRunProject.Runtime;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class LaserLevelUpEvent : ILevelUpEvent
    {
        enum LevelUpType
        {
            [InspectorName("クールタイム減少")] ReduceCoolTime,
            [InspectorName("レーザーの数を増やす")] IncreaseLaserAmount,
            [InspectorName("1秒あたりのダメージ量を増やす")] IncreaseDamageOverTime
        }
        [SerializeField] private LevelUpType _levelUpType;
        [SerializeField, CustomLabel("シールドのクールタイムを減少 -% (現在のクールタイムから)"), 
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.ReduceCoolTime)] private float _reduceCoolTime;
        [SerializeField, CustomLabel("増やすレーザーの数"), 
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreaseLaserAmount)] private int _increaseLaserAmount;
        [SerializeField, CustomLabel("増やすダメージ量(damage / s)"), 
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreaseDamageOverTime)] private float _increaseDamageOverTime;
        public void LevelUp(in ISkillParameter skillParameter)
        {
            if (skillParameter is LaserSkillParameter param)
            {
                switch (_levelUpType)
                {
                    case LevelUpType.ReduceCoolTime:
                        param.CoolTime *= (100 - _reduceCoolTime) / 100;
                        break;
                    case LevelUpType.IncreaseLaserAmount:
                        param.Amount += _increaseLaserAmount;
                        break;
                    case LevelUpType.IncreaseDamageOverTime:
                        param.DamageOverTime += _increaseDamageOverTime;
                        break;
                }
            }
            else
            {
                Debug.LogError($"{nameof(LaserSkillParameter)}にキャストできませんでした");
            }
        }
    }
}