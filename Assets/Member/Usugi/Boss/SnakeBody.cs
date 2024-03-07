using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class SnakeBody : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;
        private Queue<Vector3> positions = new Queue<Vector3>();
        public float stopDistance = 0.1f; // 動きを停止する距離の閾値
        private Vector3 lastPosition; // 前フレームのターゲットの位置
        public float movementThreshold = 0.01f; // ターゲットが「動いていない」と判断する速度の閾値

        void Start()
        {
            // 初期化時にターゲットの位置を記録
            lastPosition = target.position;
        }

        void Update()
        {
            // ターゲットの現在の速度を計算
            float speed = Vector3.Distance(target.position, lastPosition) / Time.deltaTime;

            // ターゲットが動いている場合のみ追尾処理を行う
            if (speed > movementThreshold)
            {
                if (Vector3.Distance(transform.position, target.position) > stopDistance)
                {
                    // 前の位置を記録
                    positions.Enqueue(target.position);
                    
                    if (positions.Count > 10) // 10は体の長さに応じて調整してください
                    {
                        transform.position = Vector3.Lerp(transform.position, positions.Dequeue() + offset, smoothSpeed);
                    }
                }
            }

            // 現フレームの位置を記録して次のフレームで使用
            lastPosition = target.position;
        }
    }
}
