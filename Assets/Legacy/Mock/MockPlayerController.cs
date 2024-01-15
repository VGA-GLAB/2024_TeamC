using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class MockPlayerController : MonoBehaviour
    {
        [SerializeField] NumRange _horiMoveLimit;
        [SerializeField] float _speed;

        //ジャンプ
        public float jumpHeight = 5.0f;
        public float jumpSpeed = 5.0f;
        private bool isJumping = false;
        private float startz;

        private void Start()
        {
            startz = transform.position.z;
        }

        private void Update()
        {
            Jump();
        }

        private void FixedUpdate()
        {
            MovePlayer();
           // Jump();

            if (transform.position.x < _horiMoveLimit.Min)
            {
                transform.position = new Vector3(_horiMoveLimit.Min, transform.position.y, transform.position.z);
            }
            
            if (transform.position.x > _horiMoveLimit.Max)
            {
                transform.position = new Vector3(_horiMoveLimit.Max, transform.position.y, transform.position.z);
            }

            transform.position = new Vector3(transform.position.x, transform.position.y, startz);
        }

        private void MovePlayer()
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * _speed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.left * -_speed;
            }
        }

        private void Jump()
        {
            // ジャンプキーを押したとき
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                isJumping = true;
                this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }
            // ジャンプ中の処理
            if (isJumping)
            { 
                if (this.transform.position.y <= 0.75)
                {
                    isJumping = false;
                }
            }

        }
    }
}
