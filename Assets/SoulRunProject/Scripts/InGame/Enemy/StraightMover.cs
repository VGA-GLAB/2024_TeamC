
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class StraightMover : IEntityMover
    {
        private float _moveSpeed;
        public void GetMoveStatus(Status status)
        {
            _moveSpeed = status.MoveSpeed;
        }

        public void OnStart()
        {
            
        }

        public void OnUpdateMove(Transform self, Transform target)
        {
            self.position += -Vector3.forward * (_moveSpeed * Time.deltaTime);
        }

        public void Stop()
        {
            
        }
    }
}
