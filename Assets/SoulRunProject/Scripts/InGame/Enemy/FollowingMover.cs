using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    ///     対象を追従するように移動させるためのクラス
    /// </summary>
    public class FollowingMover : IEntityMover
    {
        [SerializeField] [Tooltip("垂直移動が可能かどうか")] bool _canVerticalMove;
        float _moveSpeed;
        bool _isStopped;

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
            Vector3 selfPos = self.position;
            Vector3 targetPos = target.position;
            if(!_canVerticalMove) targetPos.y = selfPos.y;  //  ターゲットの座標の高さを敵の位置と合わせる(水平方向)
            //  自分とターゲットの座標を基準に移動
            self.position = Vector3.MoveTowards(selfPos, targetPos, _moveSpeed * Time.deltaTime);
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