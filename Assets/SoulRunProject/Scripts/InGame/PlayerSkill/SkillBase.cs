using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// スキルの基底クラス
    /// </summary>
    public abstract class SkillBase
    {
        /// <summary>スキルのレベル(0スタート)</summary>
        int _level;

        readonly LevelUpTable _levelUpTable;
        readonly string _className;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected SkillBase(string className)
        {
            _className = className;
            var temp = Resources.Load<LevelUpTable>($"LevelUpTable/{className}");
            if (temp == null)
            {
                Debug.LogError($"{className}のレベルアップテーブルが見つかりませんでした。");
            }
            else
            {
                _levelUpTable = temp;
                //  レベル1のパラメーターを割り当てる。
                SkillParameter = _levelUpTable.SkillParameters[0];
            }
        }

        /// <summary>スキルのパラメーター</summary>
        public SkillParameter SkillParameter { get; private set; }

        /// <summary>スキルの最大レベル(1スタート)</summary>
        public int MaxLevel { get; } = 5;

        /// <summary>スキル起動</summary>
        public abstract void Fire();

        /// <summary>スキル停止</summary>
        public abstract void Stop();

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