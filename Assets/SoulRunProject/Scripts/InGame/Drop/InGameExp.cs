using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 経験値
    /// </summary>
    public class InGameExp : DropBase
    {
        [SerializeField, CustomLabel("このドロップを拾うことで得られる経験値量")] private int _exp;
        protected override void PickUp(PlayerManager playerManager)
        {
            playerManager.GetExp(_exp);
            FinishedSubject.OnNext(Unit.Default);
        }
    }
}
