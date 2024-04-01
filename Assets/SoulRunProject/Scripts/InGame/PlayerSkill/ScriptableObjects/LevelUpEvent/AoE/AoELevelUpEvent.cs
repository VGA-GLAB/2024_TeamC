using System;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("抽象クラス")]
    public abstract class AoELevelUpEvent : ILevelUpEvent
    {
        public void LevelUp(in SkillParameterBase skillParameterBase)
        {
            if (skillParameterBase is AoESkillParameter param)
            {
                LevelUpParam(in param);
            }
            else
            {
                Debug.LogError($"{nameof(AoESkillParameter)}にキャストできませんでした");
            }
        }
        public abstract void LevelUpParam(in AoESkillParameter param);
    }
    
    [Serializable, Name("ダメージ量上昇")]
    public class LevelUpEventAoEAttackDamage : AoELevelUpEvent
    {
        [SerializeField, Header("1秒間で与えるダメージ増加")] float _increaseDamage;

        public override void LevelUpParam(in AoESkillParameter param)
        {
            param.AttackDamage += _increaseDamage;
        }
    }
    [Serializable, Name("AoE範囲上昇")]
    public class LevelUpEventAoERange : AoELevelUpEvent
    {
        [SerializeField, Header("範囲をどれだけ増加させるか")] float _increaseRange;

        public override void LevelUpParam(in AoESkillParameter param)
        {
            param.Range += _increaseRange;
        }
    }
}