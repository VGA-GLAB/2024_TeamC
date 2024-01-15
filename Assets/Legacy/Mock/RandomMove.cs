using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class RandomMove : MonoBehaviour
    {
        public float forwardSpeed = 5f;
        public float strafeSpeed = 3f;
        public float maxTime = 2f;
        public float minTime = 0.5f;

        [SerializeField] private float timer;
        private float timeToChangeDirection;
        private float randomDirection;

        private void Start()
        {
            ResetTimer();
        }

        private void Update()
        {
            // 前進
            transform.position += Vector3.forward * -forwardSpeed * Time.deltaTime;
            transform.position += Vector3.right * randomDirection * strafeSpeed * Time.deltaTime;

            // 時間が経過したら方向転換
            timer += Time.deltaTime;
            if (timer > timeToChangeDirection)
            {
                ChangeDirection();
                ResetTimer();
            }

            if (transform.position.z < -10)
            {
                Destroy(gameObject);
            }
        }

        private void ChangeDirection()
        {
            // 左右どちらかにランダムに移動
            randomDirection = Random.Range(-1, 2); // -1, 0, 1
        }

        private void ResetTimer()
        {
            timer = 0f;
            timeToChangeDirection = Random.Range(minTime, maxTime);
        }
    }
}
