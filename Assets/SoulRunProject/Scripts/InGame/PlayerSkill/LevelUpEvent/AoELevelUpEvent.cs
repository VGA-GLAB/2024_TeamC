using System;
using SoulRunProject.Runtime;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class AoELevelUpEvent : ILevelUpEvent
    {
        enum LevelUpType
        {
            [InspectorName("ダメージ量上昇")] IncreaseAttackDamage,
            [InspectorName("AoE範囲上昇")] IncreaseRange,
        }

        [SerializeField] private LevelUpType _levelUpType;
        [SerializeField, CustomLabel("1秒間で与えるダメージ増加"), ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreaseAttackDamage)] 
        float _increaseDamage;
        [SerializeField, CustomLabel("範囲をどれだけ増加させるか"), ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreaseRange)] 
        float _increaseRange;
        public void LevelUp(in ISkillParameter skillParameter)
        {
            if (skillParameter is AoESkillParameter param)
            {
                switch (_levelUpType)
                {
                    case LevelUpType.IncreaseAttackDamage :
                        param.BaseAttackDamage += _increaseDamage;
                        break;
                    case LevelUpType.IncreaseRange :
                        param.BaseSize += _increaseRange;
                        break;
                }
            }
            else
            {
                Debug.LogError($"{nameof(AoESkillParameter)}にキャストできませんでした");
            }
        }
    }
}