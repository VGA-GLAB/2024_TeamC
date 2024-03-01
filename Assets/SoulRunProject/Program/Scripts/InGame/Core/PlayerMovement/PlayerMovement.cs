using UnityEngine;

namespace SoulRunProject.InGame.PlayerMovement
{
    /// <summary>
    /// プレイヤー移動
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float _moveSpeed;
        [SerializeField] float _jumpPower;
        [SerializeField] float _grav;
 
        Rigidbody _rb;
        bool _isGround;
        private bool _jumped;
        float _velocityY = 0;

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
                    _velocityY = -_grav;
                }
            }

            _rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * _moveSpeed, _velocityY, 0);
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
    }
}
