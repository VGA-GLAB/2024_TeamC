using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// 障害物を動かす
    /// </summary>
    public class ObstacleMover : MonoBehaviour
    {
        /// <summary> 移動速度 </summary>
         [SerializeField] float _moveSpeed = 1;

        private void FixedUpdate()
        {
            MoveForward();
        }

        /// <summary>
        /// カメラ側に進んでいく
        /// </summary>
        private void MoveForward()
        {
            transform.position += new Vector3(0, 0, -1 * _moveSpeed); //手前に進むので-1
        }
    }
}
