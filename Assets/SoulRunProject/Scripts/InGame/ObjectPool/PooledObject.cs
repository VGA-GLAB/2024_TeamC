using System;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// オブジェクトプールで管理されるコンポーネント
    /// </summary>
    public abstract class PooledObject : MonoBehaviour
    {
        readonly Subject<Unit> _finishedSubject = new();
        public IObservable<Unit> OnFinishedAsync => _finishedSubject.Take(1);
        public bool IsPooled { get; set; }

        /// <summary>
        /// インスタンスが持っているパラメーターや状態を初期化する。
        /// </summary>
        public abstract void Initialize();
        /// <summary>
        /// Finish()が呼ばれた時に呼ばれる
        /// </summary>
        public virtual void OnFinish() {}
        /// <summary>
        /// オブジェクトをプールに戻す。プールされていなければ普通に破棄する。
        /// </summary>
        public void Finish()
        {
            if (IsPooled)
            {
                OnFinish();
                _finishedSubject.OnNext(Unit.Default);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void OnDestroy()
        {
            _finishedSubject.Dispose();
        }
    }
}
