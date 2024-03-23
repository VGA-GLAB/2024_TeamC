using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    public interface ISkill
    {
        public void Fire();

        public void UpdateSkill(float deltaTime);
        
    }
    
    /// <summary>
    /// スキルの基底クラス
    /// </summary>
    [Serializable]
    public class SkillBase : ISkill
    {
        [SerializeField, Header("スキルの名前")] private string _skillName;
        [SerializeField, Header("レベルアップイベントデータ")] protected ProjectileSkillLevelUpEvent ProjectileSkillLevelUpEvent;
        [SerializeField , Header("スキルのパラメーターデータ")] protected SkillParameterBase SkillBaseParam;

        public string SkillName => _skillName;

        private readonly string _className;
        

        /// <summary>スキルの最大レベル(1スタート)</summary>
        private int _currentLevel = 1; 
        public int MaxLevel { get; } = 5;

        private float _currentCoolTime;
    
        public virtual void UpdateSkill(float deltaTime)
        {
            if (_currentCoolTime < SkillBaseParam.CoolTime)
            {
                _currentCoolTime += deltaTime;
            }
            else
            {
                Fire();
                LevelUp();
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
            //  現在のレベルが最大レベル-1より小さければ
            if (_currentLevel <= MaxLevel)
            {
                ProjectileSkillLevelUpEvent.LevelUp(_currentLevel , SkillBaseParam);
                
            }
        }
    }
}