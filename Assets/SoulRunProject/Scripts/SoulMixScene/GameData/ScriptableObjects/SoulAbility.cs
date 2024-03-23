using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    [CreateAssetMenu(fileName = "SoulAbility", menuName = "SoulRunProject/SoulAbility")]
    public class SoulAbility : ScriptableObject
    {
        // 技名
        [SerializeField] private string abilityName;

        public string AbilityName
        {
            get => abilityName;
            set => abilityName = value;
        }

        // クールタイム
        [SerializeField] private float coolTime;

        public float CoolTime
        {
            get => coolTime;
            set => coolTime = value;
        }

        // 技効果
        [SerializeField] private SkillBase skillBase;

        public SkillBase SkillBase
        {
            get => skillBase;
            set => skillBase = value;
        }

        // 効果テキスト
        [SerializeField] private string effectText;

        public string EffectText
        {
            get => effectText;
            set => effectText = value;
        }
    }
}