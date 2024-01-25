using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    [System.Serializable]
    public class SoulAbility
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