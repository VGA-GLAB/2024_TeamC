using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    [CreateAssetMenu(fileName = "SoulAbility", menuName = "SoulRunProject/SoulAbility")]
    public class SoulAbility : ScriptableObject
    {
        // 技名
        [SerializeField] private string abilityName;

        // クールタイム
        [SerializeField] private float coolTime;

        // 技効果
        [SerializeField] private string effect;

        // 効果テキスト
        [SerializeField] private string effectText;
    }
}