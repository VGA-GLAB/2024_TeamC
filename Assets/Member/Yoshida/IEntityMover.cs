using UnityEngine;

namespace SoulRun.InGame
{
    public interface IEntityMover
    {
        public void GetMoveStatus(float moveSpeed);
        public void Move();
    }
}
