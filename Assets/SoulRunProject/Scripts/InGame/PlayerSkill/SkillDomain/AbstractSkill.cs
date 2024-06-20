using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public abstract class AbstractSkill
    {
        protected PlayerManager _playerManagerInstance;
        protected Transform _playerTransform;
        protected AbstractSkillData _skillData;
        protected ISkillParameter _runtimeParameter;
        public PlayerSkill SkillType => _skillData.SkillType;
        public AbstractSkillData AbstractSkillData => _skillData;
        protected AbstractSkill(AbstractSkillData skillData, in PlayerManager playerManager, in Transform playerTransform)
        {
            _skillData = skillData;
            _playerManagerInstance = playerManager;
            _playerTransform = playerTransform;
            _runtimeParameter = skillData.Parameter.Clone();
            _runtimeParameter.SetPlayerStatus(playerManager.CurrentPlayerStatus);
        }
        public int CurrentLevel { get; private set; } = 1;
        public bool CanLevelUp => CurrentLevel <= _skillData.MaxSkillLevel;
        public abstract void StartSkill();

        public abstract void UpdateSkill(float deltaTime);

        /// <summary>レベルアップ時イベント</summary>
        public abstract void OnLevelUp();
        /// <summary>スキル進化</summary>
        public void LevelUp()
        {
            CurrentLevel++;
            if (CanLevelUp)
            {
                LevelUpParameter(CurrentLevel);
                OnLevelUp();
            }
            else
            {
                Debug.LogError("レベル上限を超えています。");
            }
        }
        void LevelUpParameter(int level)
        {
            //　2レベルになってからレベルテーブルを使うため。
            int levelIndex = level - 2;
            if (levelIndex　<= _skillData.LevelUpTable.Count)
            {
                foreach (var levelUpEvent in _skillData.LevelUpTable[levelIndex])
                {
                    levelUpEvent.LevelUp(_runtimeParameter);
                }
            }
            else
            {
                Debug.LogError($"レベルアップテーブルのインデックス{levelIndex}番目は設定されていません。");
            }
        }
    }
}