using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// コイン
    /// </summary>
    public class InGameCoin : DropBase
    {
        [SerializeField] private int _coin;
        protected override void PickUp(PlayerManager playerManager)
        {
            playerManager.ResourceContainer.Coin += _coin;
            FinishedSubject.OnNext(Unit.Default);
            CriAudioManager.Instance.PlaySE(CriAudioManager.CueSheet.Se, "SE_CoinGet");
        }
    }
}
