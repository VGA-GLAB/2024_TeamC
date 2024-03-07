using SoulRunProject.SoulMixScene;
using UnityEngine;
using VContainer;

namespace SoulRunProject.InGame
{
    /// <summary>
    ///     対象を追従するように移動させるためのクラス
    /// </summary>
    public class FollowingMover : IEntityMover
    {
        [SerializeField] [Tooltip("プレイヤーの方向を向く速さ")]
        float _aimSpeed = 1f;

        [SerializeField] [Tooltip("プレイヤーに近づいたと認識する距離")]
        float _nearDistance = 3f;

        [SerializeField] [Tooltip("プレイヤーに近づいてから直進し始めるまでの時間(s)")]
        float _straightMoveTime = 3f;

        Vector3 _initForward;
        float _moveSpeed, _straightMoveTimer;
        [Inject] IPlayerReference _playerReference;
        bool _straightMove, _initialized;

        public void GetMoveStatus(Status status)
        {
            _moveSpeed = status.MoveSpeed;
        }

        /// <summary>
        ///     移動処理、FixedUpdateで呼ぶ。
        /// </summary>
        /// <param name="self">自身のTransform</param>
        public void Move(Transform self, Rigidbody rb)
        {
            if (!_initialized)  //  初期化されていなければ最初のForwardを登録する。
            {
                _initForward = self.forward;
                _initialized = true;
            }

            if (!_straightMove) //  プレイヤーを追う。
            {
                var direction = _playerReference.Player.position - self.position;
                direction.y = 0;
                rb.velocity = direction.normalized * _moveSpeed; //  対象の方向に移動
                self.forward = Vector3.Slerp(self.forward, direction, _aimSpeed * Time.fixedDeltaTime); //  徐々に回転
            }
            else // 最初に向いていた方向に直進させる。
            {
                rb.velocity = _initForward * _moveSpeed;
                self.forward = Vector3.Slerp(self.forward, _initForward, _aimSpeed * Time.fixedDeltaTime); //  徐々に回転
            }
            //  対象に近ければタイマーを加算する。
            if ((_playerReference.Player.position - self.position).sqrMagnitude <= _nearDistance * _nearDistance)
                _straightMoveTimer += Time.fixedDeltaTime;
            //  タイマーが設定時間以上になったらフラグを立てる。
            if (_straightMoveTime <= _straightMoveTimer) _straightMove = true;
        }
    }
}