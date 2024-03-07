using UnityEngine;
using SoulRunProject.SoulMixScene;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// Enemyの通常移動処理の実装クラス
    /// </summary>
    public class EntityNormalMover : IEntityMover
    {
        private float _moveSpeed;
        
        /// <summary>
        /// ステータス入手メソッド
        /// </summary>
        /// <param name="status">ステータスのScriptableObject</param>
        public void GetMoveStatus(Status status)
        {
            _moveSpeed = status.MoveSpeed;
        }

        /// <summary>
        /// 移動処理メソッド
        /// </summary>
        public void Move(Transform self, Rigidbody rb)
        {
            rb.velocity = self.forward * _moveSpeed;
        }
    }
}