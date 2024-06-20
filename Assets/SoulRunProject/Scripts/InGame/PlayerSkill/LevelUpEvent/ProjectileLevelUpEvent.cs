using System;
using SoulRunProject.Runtime;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("飛翔物レベルアップイベントクラス")]
    public class ProjectileLevelUpEvent : ILevelUpEvent
    {
        enum LevelUpType
        {
            [InspectorName("弾のクールタイムを減少")] ReduceCoolTime,
            [InspectorName("弾の発射数を増加")] IncreaseBulletCount,
            [InspectorName("弾の速度を増加")] IncreaseBulletSpeed
        }

        [SerializeField] private LevelUpType _levelUpType;
        
        [SerializeField, CustomLabel("弾のクールタイムを減少 -% (現在のクールタイムから)"), 
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.ReduceCoolTime)] private float _reduceCoolTime;
        [SerializeField, CustomLabel("弾の発射数を増加 +同時発射数"), 
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreaseBulletCount)] private int _addAmountCount;
        [SerializeField, CustomLabel("弾の速度を増加 +% (現在の速度から) "), 
         ShowWhenEnum(nameof(_levelUpType), LevelUpType.IncreaseBulletSpeed)] private float _multipleProjectionSpeed;
        public void LevelUp(in ISkillParameter skillParameter)
        {
            if (skillParameter is ProjectileSkillParameter param)
            {
                switch (_levelUpType)
                {
                    case LevelUpType.ReduceCoolTime:
                        param.BaseCoolTime *= (100 - _reduceCoolTime) / 100;
                        Debug.Log($"レベルアップでクールタイムを {param.BaseCoolTime}　にアップグレードしました");
                        break;
                    case LevelUpType.IncreaseBulletCount:
                        param.BaseAmount += _addAmountCount;
                        Debug.Log($"レベルアップで弾数を　{param.BaseAmount}　にアップグレードしました");
                        break;
                    case LevelUpType.IncreaseBulletSpeed:
                        param.BaseSpeed *= 1 + _multipleProjectionSpeed / 100 ;
                        Debug.Log($"レベルアップで弾速度を {param.BaseSpeed}　にアップグレードしました");
                        break;
                }
            }
            else
            {
                Debug.LogError("ProjectileSkillParameterにキャストできませんでした");
            }
        }
    }
}