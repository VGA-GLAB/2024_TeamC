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
        [SerializeField] [Tooltip("プレイヤーに近づいたと認識する距離")] float _nearDistance = 3f;
        [SerializeField] [Tooltip("プレイヤーに近づいてから直進し始めるまでの時間(s)")] float _straightMoveTime = 3f;
        float _moveSpeed, _straightMoveTimer;
        bool _straightMove, _isStopped;

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
            if (!_straightMove) //  プレイヤーを追う。
            {
                Vector3 selfPos = self.position;
                Vector3 targetPos = target.position;
                if(!_canVerticalMove) targetPos.y = selfPos.y;  //  ターゲットの座標の高さを敵の位置と合わせる(水平方向)
                //  自分とターゲットの座標を基準に移動
                self.position = Vector3.MoveTowards(selfPos, targetPos, _moveSpeed * Time.deltaTime);
            }
            else // 最初に向いていた方向に直進させる。
            {
                //  自分と自分の正面の座標を基準に移動
                self.position = Vector3.MoveTowards(self.position, self.position + self.forward, _moveSpeed * Time.deltaTime);
            }
            //  対象に近ければタイマーを加算する。
            if ((target.position - self.position).sqrMagnitude <= _nearDistance * _nearDistance)
                _straightMoveTimer += Time.deltaTime;
            //  タイマーが設定時間以上になったらフラグを立てる。
            if (_straightMoveTime <= _straightMoveTimer) _straightMove = true;
        }
        public void Stop()
        {
            _isStopped = true;
        }
    }
}