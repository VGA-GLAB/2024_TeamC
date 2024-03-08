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
        public void OnStart();
        /// <summary>移動処理メソッド、Updateで呼び出す</summary>
        public void OnUpdateMove(Transform self, Transform target = default);
        /// <summary>処理停止メソッド</summary>
        public void Stop();
    }
}
