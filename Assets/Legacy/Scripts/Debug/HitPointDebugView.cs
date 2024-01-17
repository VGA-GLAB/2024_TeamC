using UnityEngine;
using UnityEngine.UI;

namespace SoulRun.InGame
{
    /// <summary>
    /// 体力の値をUI上に表示するクラス
    /// </summary>
    public class HitPointDebugView : MonoBehaviour
    {
        [SerializeField] private PlayerManager player;
        [SerializeField] private Text _hpText;

        private void Update()
        {
            if (player != null && _hpText != null)
            _hpText.text = $"HP:{player.HitPoint.HP}";
        }
    }
}
