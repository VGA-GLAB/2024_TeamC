using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class SkillLevelUpEvent
    {
        //TODO リストのリスト化
        [SerializeReference, SubclassSelector, Header("レベルアップイベントタイプ")]  ILevelUpEventTableType _levelUpType;
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
}
