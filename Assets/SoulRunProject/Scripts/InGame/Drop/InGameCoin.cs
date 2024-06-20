using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// コイン
    /// </summary>
    public class InGameCoin : DropBase
    {
        [SerializeField, CustomLabel("このドロップを拾うことで得られるコイン量")]
        private int _coin;

        protected override void PickUp(PlayerManager playerManager)
        {
            playerManager.ResourceContainer.Coin += _coin;
            Finish();
            CriAudioManager.Instance.PlaySE("SE_CoinGet");
        }
    }
}