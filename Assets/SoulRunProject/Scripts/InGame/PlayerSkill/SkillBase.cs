using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// スキルの基底クラス
    /// </summary>
    public abstract class SkillBase : MonoBehaviour
    {
        /// <summary>スキルのレベル(0スタート)</summary>
        int _level; 
        [SerializeField] LevelUpTable _levelUpTable;
        readonly string _className;

        /// <summary>スキルのパラメーター</summary>
        public SkillParameter SkillParameter { get; private set; }

        /// <summary>スキルの最大レベル(1スタート)</summary>
        public int MaxLevel { get; } = 5;

        /// <summary>スキル起動</summary>
        public abstract void StartSkill();

        public abstract void UpdateSkill();

        /// <summary>スキル停止</summary>
        public abstract void StopSkill();

        /// <summary>スキル進化</summary>
        public void LevelUp()
        {
            //  現在のレベルが最大レベル-1より小さければ
            if (_level < MaxLevel - 1)
            {
                _level++;
                if (_level < _levelUpTable.SkillParameters.Count)
                {
                    SkillParameter = _levelUpTable.SkillParameters[_level];
                }
                else
                {
                    Debug.LogError($"{_className}のレベルアップテーブルのインデックス{_level}番目は設定されていません。");
                }
            }
        }
    }
}