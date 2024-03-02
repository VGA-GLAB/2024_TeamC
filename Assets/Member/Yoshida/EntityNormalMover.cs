using UnityEngine;
using SoulRunProject.SoulMixScene;

namespace SoulRun.InGame.Enemy
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
        /// 移動処理メソッド(仮)
        /// </summary>
        public void Move()
        {
            Debug.Log($"Move! value = {_moveSpeed}");
        }
    }
}