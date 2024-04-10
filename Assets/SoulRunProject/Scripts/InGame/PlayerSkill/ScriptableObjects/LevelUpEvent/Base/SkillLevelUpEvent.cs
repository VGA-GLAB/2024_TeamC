using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class SkillLevelUpEvent
    {
        //TODO リストのリスト化
        [SerializeReference, Header("レベルアップイベントタイプ")] ILevelUpEventListList _levelUpType;

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
}
