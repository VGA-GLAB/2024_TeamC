using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.Common
{
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/ProjectionSkillLevelUpEvent")]
    public class ProjectileSkillLevelUpEvent : ScriptableObject
    {
        //TODO リストのリスト化
        [SerializeField, Header("投射物レベルアップイベント")]  List<ProjectileSkillLevelUpEventList> _levelUpEvents;

        public void LevelUp(int level , SkillParameterBase currentParam)
        {
            //　2レベルになってからレベルテーブルを使うため。
            int levelIndex = level - 2;
            if (levelIndex　<= _levelUpEvents.Count)
            {
                foreach (var levelUpEvent in _levelUpEvents[levelIndex].LevelUpType)
                {
                    levelUpEvent.LevelUp(currentParam);
                }
            }
            else
            {
                Debug.LogError($"レベルアップテーブルのインデックス{levelIndex}番目は設定されていません。");
            }
        }
    }

    [Serializable]
    public class ProjectileSkillLevelUpEventList
    {
        [SerializeReference, SubclassSelector, Header("投射物レベルアップイベント")]  List<IProjectionLevelUp> _levelUpType;
        public List<IProjectionLevelUp> LevelUpType => _levelUpType;
    }
    [Serializable]
    public abstract class IProjectionLevelUp
    {
        public virtual void LevelUp(in SkillParameterBase skillParameterBase)
        {
            if (skillParameterBase is ProjectileSkillParameter param)
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
    
    [Serializable]
    public class LevelUpProjectionCoolTime : IProjectionLevelUp
    {
        [SerializeField , Header("弾のクールタイムを除算 -% (現在のクールタイムから)")] private float _reduceCoolTime;

        public override void LevelUpParam(in ProjectileSkillParameter param)
        {
            param.CoolTime *= (100 - _reduceCoolTime) / 100;
            Debug.Log($"レベルアップでクールタイムを {param.CoolTime}　にアップグレードしました");
        }
    }
    
    [Serializable]
    public class LevelUpProjectionAmount : IProjectionLevelUp
    {
        [SerializeField , Header(" 弾の発射数を増加 +同時発射数")] private int _addAmountCount;
        public override void LevelUpParam(in ProjectileSkillParameter param)
        {
            param.Amount += _addAmountCount;
            Debug.Log($"レベルアップで弾数を　{param.Amount}　にアップグレードしました");
        }
    }
    
    [Serializable]
    public class LevelUpProjectionSpeed : IProjectionLevelUp
    {
        [SerializeField , Header("弾の速度を乗算 +% (現在の速度から) ")] private float _multipleProjectionSpeed;
        public override void LevelUpParam(in ProjectileSkillParameter param)
        {
            param.Speed *= 1 + _multipleProjectionSpeed / 100 ;
            Debug.Log($"レベルアップで弾速度を {param.Speed}　にアップグレードしました");
        }
    }
}
