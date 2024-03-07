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
        [SerializeField, Tooltip("プレイヤーの方向を向く速さ")] float _aimSpeed = 1f;
        [Inject] IPlayerReference _playerReference;
        float _moveSpeed;
        bool _isStopped;

        public void GetMoveStatus(Status status)
        {
            _moveSpeed = status.MoveSpeed;
        }
        public void Move(Transform self)
        {
            if (_isStopped) return;
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