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
        bool _isStopped;
        
        /// <summary>
        /// ステータス入手メソッド
        /// </summary>
        /// <param name="status">ステータスのScriptableObject</param>
        public void GetMoveStatus(Status status)
        {
            _moveSpeed = status.MoveSpeed;
        }
        public void Move(Transform self)
        {
            if (_isStopped) return;
            self.position = Vector3.MoveTowards(self.position, self.forward, _moveSpeed * Time.deltaTime);
        }

        public void Stop()
        {
            _isStopped = true;
        }
    }
}