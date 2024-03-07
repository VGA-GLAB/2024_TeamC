using System;
using Unity.Mathematics;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤー移動
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _grav;
        [SerializeField, Tooltip("position.x制限のx下限、y上限")] private Vector2 _moveRange;

        private Rigidbody _rb;
        private bool _isGround;
        private Vector3 _playerVelocity;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
        }

        private void Update()
        {
            if (_isGround && Input.GetButtonDown("Jump"))
            {
                _playerVelocity.y = _jumpPower;
            }

            _playerVelocity.x = Input.GetAxisRaw("Horizontal") * _moveSpeed;
            LimitPosition();
            _rb.velocity = _playerVelocity;
        }

        private void FixedUpdate()
        {
            if (_isGround && _playerVelocity.y <= 0)
            {
                _playerVelocity.y = 0;
            }
            else
            {
                _playerVelocity.y -= _grav * Time.fixedDeltaTime;
            }

            _isGround = false;
        }

        private void OnCollisionStay(Collision other)
        {
            for (int i = 0; i < other.contactCount; i++)
            {
                if (Vector3.Angle(Vector3.up, other.GetContact(i).normal) < 45)
                {
                    _isGround = true;
                }
            }
        }

        /// <summary>
        /// プレイヤーのポジションを一定範囲内に限定する
        /// </summary>
        void LimitPosition()
        {
            // x マイナス側の制限
            if (transform.position.x <= _moveRange.x)
            {
                // 位置の制限
                Vector3 pos = transform.position;
                pos.x = _moveRange.x;
                transform.position = pos;
                // Velocityの制限
                _playerVelocity.x = Mathf.Clamp(_playerVelocity.x, 0, _moveSpeed);
                return;
            }

            // x プラス側の制限
            if (transform.position.x >= _moveRange.y)
            {
                // 位置の制限
                Vector3 pos = transform.position;
                pos.x = _moveRange.y;
                transform.position = pos;
                // Velocityの制限
                _playerVelocity.x = Mathf.Clamp(_playerVelocity.x, -_moveSpeed, 0);
            }
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Vector3 posX = Vector3.right * _moveRange.x;
            Vector3 posY = Vector3.right * _moveRange.y;
            Gizmos.DrawLine(posX, posY);
            Gizmos.DrawLine(posX + Vector3.up, posX - Vector3.up);
            Gizmos.DrawLine(posY + Vector3.up, posY - Vector3.up);
        }
        #endif
    }
}
