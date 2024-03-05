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
        [Header("Check Field")] 
        [SerializeField, Tooltip("フィールドの端でそれ以上進めなくなる距離")] private float _limitDistance = 0.5f;
        [SerializeField] private Vector3 _boxSize = new Vector3(0, 10, 0);
        [SerializeField] private float _distance = 10;

        private Rigidbody _rb;
        private bool _isGround;
        private bool _jumped;
        private float _velocityY;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
        }

        private void Update()
        {
            if (_isGround)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    _velocityY = _jumpPower;
                    _isGround = false;
                    _jumped = true;
                    CancelInvoke(nameof(CancelJumped));
                    Invoke(nameof(CancelJumped), Time.fixedDeltaTime * 3);
                }
                else
                {
                    _velocityY = 0;
                }
            }

            _rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * _moveSpeed, _velocityY, 0);
            RelocateBasedOnField();
            Debug.Log(transform.position.x);
        }

        private void FixedUpdate()
        {
            if (!_isGround)
            {
                _velocityY -= _grav * Time.fixedDeltaTime;
            }

            _isGround = false;
        }

        /// <summary> jump直後は接地判定がtrueにならない </summary>
        void CancelJumped()
        {
            _jumped = false;
        }

        private void OnCollisionStay(Collision other)
        {
            for (int i = 0; i < other.contactCount; i++) // すべての接触点においてIsFloorをかける
            {
                if (Vector3.Angle(Vector3.up, other.contacts[i].normal) < 45 && !_jumped) // 地面として扱えるか
                {
                    _isGround = true;
                }
            }
        }

        /// <summary>
        /// フィールドから出ないようにプレイヤーを再配置する
        /// </summary>
        void RelocateBasedOnField()
        {
            Vector3 leftCenter = transform.position + -transform.right * _distance + -transform.up * _boxSize.y * 0.6f;
            
            if (Physics.BoxCast(leftCenter, _boxSize * 0.5f, transform.right, 
                    out RaycastHit hit, quaternion.identity, _distance)) 
            {
                if (transform.position.x - hit.point.x <= _limitDistance)
                {
                    Vector3 pos = transform.position;
                    pos.x = hit.point.x + _limitDistance;
                    transform.position = pos;
                    float veloX = Mathf.Clamp(_rb.velocity.x, 0, _moveSpeed);
                    _rb.velocity = new Vector3(veloX, _velocityY, 0);
                    return;
                }
            }
            
            Vector3 rightCenter = transform.position + transform.right * _distance + -transform.up * _boxSize.y * 0.6f;
            
            if (Physics.BoxCast(rightCenter, _boxSize * 0.5f, -transform.right, 
                    out hit, quaternion.identity, _distance)) 
            {
                if (hit.point.x - transform.position.x < _limitDistance)
                {
                    Vector3 pos = transform.position;
                    pos.x = hit.point.x - _limitDistance;
                    transform.position = pos;
                    float veloX = Mathf.Clamp(_rb.velocity.x, -_moveSpeed, 0);
                    _rb.velocity = new Vector3(veloX, _velocityY, 0);
                }
            }
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Vector3 leftCenter = transform.position + -transform.right * _distance + -transform.up * _boxSize.y * 0.6f;
            
            if (Physics.BoxCast (leftCenter, _boxSize * 0.5f, transform.right, 
                    out RaycastHit hit, quaternion.identity, _distance)) 
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay (leftCenter, transform.right * hit.distance);
                Gizmos.DrawWireCube (leftCenter + transform.right * hit.distance, _boxSize);
            } else {
                Gizmos.color = Color.red;
                Gizmos.DrawRay (leftCenter, transform.right * _distance);
            }

            Vector3 rightCenter = transform.position + transform.right * _distance + -transform.up * _boxSize.y * 0.6f;
            
            if (Physics.BoxCast (rightCenter, _boxSize * 0.5f, -transform.right, 
                    out hit, quaternion.identity, _distance)) 
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawRay (rightCenter, -transform.right * hit.distance);
                Gizmos.DrawWireCube (rightCenter - transform.right * hit.distance, _boxSize);
            } else {
                Gizmos.color = Color.red;
                Gizmos.DrawRay (rightCenter, -transform.right * _distance);
            }
        }
        #endif
    }
}
