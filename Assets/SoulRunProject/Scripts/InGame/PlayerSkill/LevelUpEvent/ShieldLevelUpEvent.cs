using System;
using SoulRunProject.Runtime;
using SoulRunProject.Skill;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// シールドスキルレベルアップイベント抽象クラス
    /// </summary>
    [Serializable, Name("シールドスキルレベルアップイベント抽象クラス")]
    public class ShieldLevelUpEvent : ILevelUpEvent
    {
        enum LevelUpType
        {
            [InspectorName("シールドのクールタイムを減少")] ReduceCoolTime,
            [InspectorName("無効化できる被ダメージ回数を増やす")] IncreaseShieldCount
        }

        [SerializeField] private LevelUpType _levelUpType;
        [SerializeField, CustomLabel("シールドのクールタイムを減少 -% (現在のクールタイムから)"), 
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.ReduceCoolTime)] private float _reduceCoolTime;
        [SerializeField, CustomLabel("増加させるシールド数"), 
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreaseShieldCount)] private int _increaseShieldCount;
        
        public void LevelUp(in ISkillParameter skillParameter)
        {
            if (skillParameter is ShieldSkillParameter param)
            {
                switch (_levelUpType)
                {
                    case LevelUpType.ReduceCoolTime :
                        param.BaseCoolTime *= (100 - _reduceCoolTime) / 100;
                        Debug.Log($"レベルアップで{nameof(ShieldSkillData)}のクールタイムを {param.BaseCoolTime}　にアップグレードしました");
                        break;
                    case LevelUpType.IncreaseShieldCount :
                        param.BaseShieldCount += _increaseShieldCount;
                        Debug.Log($"レベルアップで{nameof(ShieldSkillData)}のダメージ無効化回数を {param.BaseShieldCount}　にアップグレードしました");
                        break;
                }
            }
            else
            {
                Debug.LogError($"{nameof(ShieldSkillParameter)}にキャストできませんでした");
            }
        }
    }
}