using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject
{
    public class DriftObj : MonoBehaviour
    {
        public Transform _player; // プレイヤーのTransform
        private Vector3 originalPosition; // 元の位置
        public float driftFactor = 0.1f; // どれだけずれるかの係数

        void Start()
        {
            // オブジェクトの初期位置を保存
            _player = GameObject.Find("Player").transform;
            originalPosition = transform.position;
        }

        void Update()
        {
            // プレイヤーとの現在の距離を計算
            float distance = Vector3.Distance(transform.position, _player.position);

            // 左右の入力を取得
            float inputX = Input.GetAxis("Horizontal");

            // 入力がある場合、距離に応じてオブジェクトをずらす
            if (inputX != 0)
            {
                float driftAmount = inputX * driftFactor * distance;
                transform.position = new Vector3(originalPosition.x + driftAmount, transform.position.y, transform.position.z);
            }
            else
            {
                // 入力がなければ元の位置に戻る
                transform.position = new Vector3(
                    Mathf.Lerp(transform.position.x, originalPosition.x, Time.deltaTime * driftFactor), 
                    transform.position.y, 
                    transform.position.z);
            }
        }
    }
}
