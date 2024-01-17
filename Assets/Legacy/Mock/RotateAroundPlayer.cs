using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class RotateAroundPlayer : MonoBehaviour
    {
        public Transform target; // 回転の中心となるプレイヤー
        public float rotationSpeed = 50.0f; // 秒速度
        public float distance = 5.0f; // プレイヤーからの距離
        public float delay = 0;

        private Vector3 offset;

        void Start()
        {
            // 初期オフセットを設定
            offset = new Vector3(0, 0, distance);
        }

        void Update()
        {
            delay -= Time.deltaTime;
            if (delay > 0) return;

            // オブジェクトをプレイヤーの周囲に配置
            transform.position = target.position + offset;

            // プレイヤーの周りを回転
            transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);

            // 回転後のオフセットを更新
            offset = transform.position - target.position;
        }
    }
}
