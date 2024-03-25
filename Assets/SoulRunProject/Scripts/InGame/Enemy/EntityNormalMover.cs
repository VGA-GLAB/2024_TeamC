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

        public void OnStart()
        {
            
        }

        public void OnUpdateMove(Transform self, Transform target = default)
        {
            if (_isStopped) return;
            self.position = Vector3.MoveTowards(self.position, self.forward, _moveSpeed * Time.deltaTime);
            if (self.position.z < target.position.z)    //  プレイヤーよりz座標が後ろに行ったら
            {
                Stop();
            }
        }

        public void Stop()
        {
            _isStopped = true;
        }
    }
}