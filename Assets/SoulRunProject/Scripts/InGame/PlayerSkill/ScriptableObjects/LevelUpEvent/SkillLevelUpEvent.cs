using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoulRunProject.Common
{
    //[CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/ProjectionSkillLevelUpEvent")]
    [Serializable]
    public class SkillLevelUpEvent
    {
        //TODO リストのリスト化
        [SerializeReference, SubclassSelector, Header("レベルアップイベントタイプ")]  ILevenUpEventTableType _levelUpType;
        public void LevelUp(int level , SkillParameterBase currentParam)
        {
            //　2レベルになってからレベルテーブルを使うため。
            int levelIndex = level - 2;
            if (levelIndex　<= _levelUpType.LevelUpTable.Count)
            {
                foreach (var levelUpEvent in _levelUpType.LevelUpTable[levelIndex].LevelUpType)
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

    public interface ILevenUpEventTableType
    {
        public List<ILevelUpEventGroup> LevelUpTable { get; }
    }

    [Serializable]
    public class ProjectileSkillLevelUpEventTableType : ILevenUpEventTableType
    {
        [SerializeField, Header("レベルアップイベントテーブル")]  List<ProjectileSkillLevelUpEventGroup> _levelUpTable;
        public List<ILevelUpEventGroup> LevelUpTable => _levelUpTable.OfType<ILevelUpEventGroup>().ToList();
    }
    
    public interface ILevelUpEventGroup
    {
        public List<ILevelUpEvent> LevelUpType { get; }
    }


    [Serializable]
    public class ProjectileSkillLevelUpEventGroup : ILevelUpEventGroup
    {
        [SerializeReference, SubclassSelector, Header("投射物レベルアップイベント")]  List<ILevelUpEvent> _levelUpEventType;
        public List<ILevelUpEvent> LevelUpType => _levelUpEventType.OfType<ILevelUpEvent>().ToList();
    }

    public interface ILevelUpEvent
    {
        public void LevelUp(in SkillParameterBase skillParameterBase);
    }
    
    [Serializable]
    public abstract class ProjectileLevelUpEvent : ILevelUpEvent
    {
        public void LevelUp(in SkillParameterBase skillParameterBase)
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
    public class LevelUpEventProjectileCoolTime : ProjectileLevelUpEvent
    {
        [SerializeField , Header("弾のクールタイムを減少 -% (現在のクールタイムから)")] private float _reduceCoolTime;

        public override void LevelUpParam(in ProjectileSkillParameter param)
        {
            param.CoolTime *= (100 - _reduceCoolTime) / 100;
            Debug.Log($"レベルアップでクールタイムを {param.CoolTime}　にアップグレードしました");
        }
    }
    
    [Serializable]
    public class LevelUpEventProjectileAmount : ProjectileLevelUpEvent
    {
        [SerializeField , Header(" 弾の発射数を増加 +同時発射数")] private int _addAmountCount;
        public override void LevelUpParam(in ProjectileSkillParameter param)
        {
            param.Amount += _addAmountCount;
            Debug.Log($"レベルアップで弾数を　{param.Amount}　にアップグレードしました");
        }
    }
    
    [Serializable]
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
