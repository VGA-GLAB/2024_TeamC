using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    public enum PlayerSkill
    {
         SoulBullet = 0 ,
         HollyField = 1 ,
         SoulSword = 2 ,
         SoulShell = 3 ,
         SoulRay = 4 ,
         SoulOfHealing = 5 ,
         SoulFrame = 6 ,
    }
    
    /// <summary>
    /// スキルの基底クラス
    /// </summary>
    [Serializable]
    public abstract class SkillBase 
    {
        [SerializeField, Header("スキルの名前")] private PlayerSkill _skillType;
        [SerializeField, Header("レベルアップイベントデータ")] protected ProjectileSkillLevelUpEvent ProjectileSkillLevelUpEvent;
        [SerializeField , Header("スキルのパラメーターデータ")] protected SkillParameterBase SkillBaseParam;

        /// <summary> スキルの最大レベル </summary>
        public const int MaxSkillLevel = 5;

        private int _currentLevel = 1;
        private float _currentCoolTime;
        
        public PlayerSkill SkillType => _skillType;
        
        /// <summary> スキルレベルアップ可能かどうか </summary>
        public bool CanLevelUp()
        {
            return _currentLevel <= MaxSkillLevel;
        }

        public abstract void StartSkill();
        public virtual void UpdateSkill(float deltaTime)
        {
            if (_currentCoolTime < SkillBaseParam.CoolTime)
            {
                _currentCoolTime += deltaTime;
            }
            else
            {
                Fire();
                _currentCoolTime = 0;
            }
        }

        public virtual void Fire()
        {
            Debug.Log("発射");
        }
        
        /// <summary>スキル進化</summary>
        public void LevelUp()
        {
            _currentLevel++;
            if (CanLevelUp())
            {
                ProjectileSkillLevelUpEvent.LevelUp(_currentLevel , SkillBaseParam);
            }
            else
            {
                Debug.LogError("レベル上限を超えています。");
            }
        }
    }
}