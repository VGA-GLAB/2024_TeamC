using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("飛翔物レベルアップイベント抽象クラス")]
    public abstract class ProjectileLevelUpEvent : ILevelUpEvent
    {
        public void LevelUp(in ISkillParameter skillParameter)
        {
            if (skillParameter is ProjectileSkillParameter param)
            {
                LevelUpParam(in param);
            }
            else
            {
                Debug.LogError("ProjectileSkillParameterにキャストできませんでした");
            }
        }
        public abstract void LevelUpParam(in ProjectileSkillParameter param);
    }
    [Serializable, Name("弾のクールタイムを減少")]
    public class LevelUpEventProjectileCoolTime : ProjectileLevelUpEvent
    {
        [SerializeField, Header("弾のクールタイムを減少 -% (現在のクールタイムから)")] private float _reduceCoolTime;

        public override void LevelUpParam(in ProjectileSkillParameter param)
        {
            param.CoolTime *= (100 - _reduceCoolTime) / 100;
            Debug.Log($"レベルアップでクールタイムを {param.CoolTime}　にアップグレードしました");
        }
    }
    
    [Serializable, Name("弾の発射数を増加")]
    public class LevelUpEventProjectileAmount : ProjectileLevelUpEvent
    {
        [SerializeField , Header(" 弾の発射数を増加 +同時発射数")] private int _addAmountCount;
        public override void LevelUpParam(in ProjectileSkillParameter param)
        {
            param.Amount += _addAmountCount;
            Debug.Log($"レベルアップで弾数を　{param.Amount}　にアップグレードしました");
        }
    }
    
    [Serializable, Name("弾の速度を増加")]
    public class LevelUpEventProjectileSpeed : ProjectileLevelUpEvent
    {
        [SerializeField , Header("弾の速度を増加 +% (現在の速度から) ")] private float _multipleProjectionSpeed;
        public override void LevelUpParam(in ProjectileSkillParameter param)
        {
            param.Speed *= 1 + _multipleProjectionSpeed / 100 ;
            Debug.Log($"レベルアップで弾速度を {param.Speed}　にアップグレードしました");
        }
    }
}