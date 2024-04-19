using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class SkillLevelUpEvent
    {
        //TODO リストのリスト化
        [SerializeReference] ILevelUpEventListList _levelUpType;
        public ILevelUpEventListList LevelUpType => _levelUpType;
        public SkillLevelUpEvent(ILevelUpEventListList levelUpType)
        {
            _levelUpType = levelUpType;
        }

        public SkillLevelUpEvent() { }
        public void LevelUp(int level , ISkillParameter currentParam)
        {
            //　2レベルになってからレベルテーブルを使うため。
            int levelIndex = level - 2;
            if (levelIndex　<= _levelUpType.LevelUpEventListList.Count)
            {
                foreach (var levelUpEvent in _levelUpType.LevelUpEventListList[levelIndex].LevelUpEventList)
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

    public enum SkillLevelLabel
    {
        Level2 = 0,
        Level3 = 1,
        Level4 = 2,
        Level5 = 3,
        Level6 = 4,
        Level7 = 5,
        Level8 = 6,
        Level9 = 7,
        Level10 = 8,
        Level11 = 9,
    }

    public enum SkillLevelEventLabel
    {
        Event1 = 0,
        Event2 = 1,
        Event3 = 2,
        Event4 = 3,
        Event5 = 4,
    }
}
