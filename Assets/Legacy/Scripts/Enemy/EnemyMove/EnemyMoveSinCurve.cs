using UnityEngine;
using System.Collections;

namespace SoulRun.InGame
{
    /// <summary>
    /// ゆらゆらX軸方向に左右に移動しながら手前に進んでくる動き
    /// </summary>
    public class EnemyMoveSinCurve : EnemyMoveRoutineBase
    { 
        private float _frequency = 2f;  //サインカーブの振動の頻度
        private float _amplitude = 2f;  //サインカーブの振幅
        private float _startTime;
        private float _startZpos;
        private float _startXpos;
        private float _zDir = 1;        //Z軸にこの値を掛けて反転させる

        public override void ActivateMoveRoutine()
        {
            StopAllCoroutines();
            _startTime = Time.time;
            _startZpos = transform.position.z;
            _startXpos = transform.position.x;
            if (isActiveAndEnabled) StartCoroutine(SinCurveMove());
        }

        public override void DeactivateMoveRoutine()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// 左右に移動しながらプレイヤーに向かって進んでくる
        /// </summary>
        /// <returns></returns>
        private IEnumerator SinCurveMove()
        {
            while (true && isActiveAndEnabled)
            {
                float elapsedTime = Time.time - _startTime;
                float xPos = _startXpos + Mathf.Sin(elapsedTime * _frequency) * _amplitude;
                float zPos = _zDir * -elapsedTime * _speed + _startZpos;
                Vector3 newPosition = new Vector3(xPos, transform.position.y, zPos);
                transform.position = newPosition;
                yield return null;
            }
        }

        /// <summary>
        /// Z軸方向に進む向きを反転させる
        /// </summary>
        public void ReverseDirection()
        {
            _zDir *= -1;
            _startTime = Time.time;
            _startZpos = transform.position.z;
        }
    }
}
