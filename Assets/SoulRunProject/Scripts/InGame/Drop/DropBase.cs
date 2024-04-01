using System;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ドロップした経験値やアイテムの基底クラス
    /// </summary>
    public abstract class DropBase : MonoBehaviour
    {
        protected readonly Subject<Unit> FinishedSubject = new();
        public IObservable<Unit> OnFinishedAsync => FinishedSubject.Take(1);
        
        /// <summary>プレイヤーがドロップ品を拾った時に呼ぶ処理</summary>
        protected abstract void PickUp(PlayerManager playerManager);

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerManager playerManager))
            {
                PickUp(playerManager);
            }
        }
    }
}
