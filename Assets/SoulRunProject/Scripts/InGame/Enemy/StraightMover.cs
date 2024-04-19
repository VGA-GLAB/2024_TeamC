
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class StraightMover : EntityMover
    {
        
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
        
        public void Pause()
        {
            
        }

        public void Resume()
        {
            
        }
    }
}
