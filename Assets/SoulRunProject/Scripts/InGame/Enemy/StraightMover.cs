using System;
using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [Serializable, Name("直進移動")]
    public class StraightMover : EntityMover
    {
        public void GetMoveStatus(Status status)
        {
            _moveSpeed = status.MoveSpeed;
        }

        public override void OnStart()
        {
        }

        public override void OnUpdateMove(Transform myTransform, Transform playerTransform)
        {
            myTransform.position += Vector3.back * (_moveSpeed * Time.deltaTime);
        }
        
        public override void Pause()
        {
        }

        public override void Resume()
        {
        }
    }
}