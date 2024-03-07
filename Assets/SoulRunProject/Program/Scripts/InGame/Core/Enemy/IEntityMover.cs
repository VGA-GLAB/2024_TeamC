using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Enemyの移動処理のインターフェース
    /// </summary>
    public interface IEntityMover
    {
        public void GetMoveStatus(Status status);
        /// <summary>移動処理メソッド、Updateで呼び出す</summary>
        public void Move(Transform self);
        /// <summary>処理停止メソッド</summary>
        public void Stop();
    }
}
