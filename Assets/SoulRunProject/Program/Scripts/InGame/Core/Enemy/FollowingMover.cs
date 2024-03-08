using SoulRunProject.Common;
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
        
        [Inject] IPlayerReference _playerReference;
        Vector3 _initForward;
        float _moveSpeed, _straightMoveTimer;
        bool _straightMove, _initialized, _isStopped;

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
            if (!_initialized)  //  初期化されていなければ最初のForwardを登録する。
            {
                _initForward = self.forward;
                _initialized = true;
            }

            if (!_straightMove) //  プレイヤーを追う。
            {
                Following(self);
            }
            else // 最初に向いていた方向に直進させる。
            {
                self.position = Vector3.MoveTowards(self.position, self.position + _initForward, _moveSpeed * Time.deltaTime);
                self.forward = Vector3.Slerp(self.forward, _initForward, _aimSpeed * Time.deltaTime); //  徐々に回転
            }
            //  対象に近ければタイマーを加算する。
            if ((_playerReference.Player.position - self.position).sqrMagnitude <= _nearDistance * _nearDistance)
                _straightMoveTimer += Time.deltaTime;
            //  タイマーが設定時間以上になったらフラグを立てる。
            if (_straightMoveTime <= _straightMoveTimer) _straightMove = true;
        }
        /// <summary>
        /// 追従移動
        /// </summary>
        void Following(Transform self)
        {
            Vector3 selfPos = self.position;
            Vector3 playerPos = _playerReference.Player.position;
            playerPos.y = selfPos.y;
            //  移動
            self.position = Vector3.MoveTowards(selfPos, playerPos, _moveSpeed * Time.deltaTime);
            //  回転
            var direction = playerPos - selfPos;
            direction.y = 0;
            self.forward = Vector3.Slerp(self.forward, direction, _aimSpeed * Time.deltaTime);
        }
        public void Stop()
        {
            _isStopped = true;
        }
    }
}