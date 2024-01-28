using System;
using UnityEngine;

namespace SoulRunProject.InGameTest
{
    /// <summary>
    /// プレイヤー移動
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float _moveSpeed;
        [SerializeField] float _jumpPower;
        [SerializeField] float _gravPower;
 
        Rigidbody _rb;
        bool _isGround;
        float _velocityY = 0;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //float velocityY = _rb.velocity.y;

            if (!_isGround)
            {
                _velocityY -= _gravPower;
            }

            if (_isGround && Input.GetButtonDown("Jump"))
            {
                _velocityY = _jumpPower;
            }

            _rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * _moveSpeed, _velocityY, 0);
        }

        private void OnCollisionStay(Collision other)
        {
            float delay = 3f; // それぞれfalseにするフレーム数

            for (int i = 0; i < other.contactCount; i++) // すべての接触点においてIsFloorをかける
            {
                Vector3 normal = other.contacts[i].normal; // 接している面の法線ベクトル

                if (Vector3.Angle(Vector3.up, normal) < 45) // 地面として扱えるか
                {
                    _isGround = true;
                    CancelInvoke(nameof(StopGrounded));
                    Invoke(nameof(StopGrounded), Time.deltaTime * delay);
                }
            }
        }
        void StopGrounded()
        {
            _isGround = false;
        }
    }
}
