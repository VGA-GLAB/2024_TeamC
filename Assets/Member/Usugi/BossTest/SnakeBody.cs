using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject
{
    public class SnakeBody : MonoBehaviour
    {
        public Transform target;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;
        private List<Vector3> positions;
        public float stopDistance = 0.1f; // 動きを停止する距離の閾値

        void Start()
        {
            positions = new List<Vector3>();
        }

        void Update()
        {
            // 前のオブジェクトが十分に動いたかどうかをチェック
            if (Vector3.Distance(transform.position, target.position) > stopDistance)
            {
                // 前の位置を記録
                positions.Insert(0, target.position);

                // 体の部分を移動させる
                if (positions.Count > 10) // 10は体の長さに応じて調整してください
                {
                    transform.position = Vector3.Lerp(transform.position, positions[positions.Count - 1] + offset, smoothSpeed);
                    positions.RemoveAt(positions.Count - 1);
                }
            }
        }
    }
}
