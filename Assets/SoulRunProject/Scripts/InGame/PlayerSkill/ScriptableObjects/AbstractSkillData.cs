using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    ///     スキルの基底クラス
    /// </summary>
    [Serializable]
    [Name("スキルの基底クラス")]
    public abstract class AbstractSkillData : ScriptableObject
    {
        //  メンバー変数
        [Header("===基本情報===")]
        [SerializeField, CustomLabel("スキルのID")] private PlayerSkill _skillType;
        [SerializeField, CustomLabel("スキルの名前")] private string _skillName;
        [SerializeField, CustomLabel("スキルの説明文")] private string _explanatoryText;
        [SerializeField, CustomLabel("スキルのアイコン")] private Sprite _skillIcon;
        [SerializeField, CustomLabel("スキルの最大レベル")] private int _maxSkillLevel = 5;
        [Header("===派生情報===")]
        [SerializeReference, CustomLabel("スキルレベル1の時のパラメーター")] protected ISkillParameter _skillParameter;
        private int _elementCount;
        //  プロパティ
        public abstract List<List<ILevelUpEvent>> LevelUpTable { get; }
        public PlayerSkill SkillType => _skillType;
        public string SkillName => _skillName;
        public string ExplanatoryText => _explanatoryText;
        public Sprite SkillIcon => _skillIcon;
        public int MaxSkillLevel => _maxSkillLevel;
        public ISkillParameter Parameter => _skillParameter;
    }
}