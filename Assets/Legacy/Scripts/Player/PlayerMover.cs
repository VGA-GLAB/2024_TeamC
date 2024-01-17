using UnityEngine;
using Cysharp.Threading.Tasks;

namespace SoulRun.InGame
{
    /// <summary>
    /// プレイヤーを動かす
    /// </summary>
    public class PlayerMover : MonoBehaviour, IAlphaPausable
    {
        /// <summary> 移動スピード </summary>
        [SerializeField] float _verticalMoveSpeed;
        [SerializeField] float _horizontalMoveSpeed;
        [SerializeField] float _horizontalInputNum = 0;
        /// <summary> ジャンプスピード </summary>
        [SerializeField] float _jumpSpeed;
        /// <summary> ジャンプ時間 </summary>
        float _jumpTime = 0.98f;
        /// <summary> ジャンプ中か </summary>
        bool _isJumping = false;
        /// <summary> ジャンプ係数 </summary>
        float _jumpingCoefficientg = 1;
        /// <summary> 重力係数 </summary>
        float _gravityCoefficientg = 1;
        /// <summary> 左右の移動限界 </summary>
        [SerializeField] NumRange _horizontalMoveLimit = new NumRange(-5, 5);
        /// <summary> 上下の移動限界 </summary>
        [SerializeField] NumRange _verticalMoveLimit = new NumRange(0.5f, 5);
        [SerializeField] bool _pause = false;
        private Vector3 _prePos;
        public float Speed => _verticalMoveSpeed;

        public void ManualUpdate(float deltaTime)
        {
            if (_pause) return;
            MoveRightOrLeft(deltaTime);
            CalcuJump(deltaTime);
            NormalizePos();
        }

        public void MoveForward(float deltaTime)
        {
            if (_pause) return;
            transform. position += new Vector3(0, 0, _verticalMoveSpeed * deltaTime);
        }

        /// <summary>
        /// 左右方向の入力を与える関数
        /// </summary>
        public void SetHorizontalInput(float x = 0)
        {
            Mathf.Clamp01(x);
            _horizontalInputNum = x * Time.deltaTime;
        }

        /// <summary>
        /// 画面上を左右に移動する
        /// </summary>
        private void MoveRightOrLeft(float deltaTime)
        {
            transform.position += new Vector3(_horizontalInputNum * _horizontalMoveSpeed * deltaTime, 0, 0);
        }
    
        /// <summary>
        /// ジャンプアクションの発火関数
        /// </summary>
        /// <returns></returns>
        public async UniTask DoJump()
        {
            if (_isJumping) return;
            _jumpingCoefficientg = 1;
            _isJumping = true;
            await UniTask.Delay(System.TimeSpan.FromSeconds(_jumpTime));
            _gravityCoefficientg = 0;
            _isJumping = false;
        }

        /// <summary>
        /// 上にジャンプする
        /// </summary>
        private void CalcuJump(float deltaTime)
        {
            if (_isJumping)
            {
                transform.position += new Vector3(0, _jumpSpeed * deltaTime * _jumpingCoefficientg, 0);
            }
            else
            {
                transform.position -= new Vector3(0, _jumpSpeed * deltaTime * _gravityCoefficientg, 0);
            }
            CalcuGravityCoe(deltaTime);
            CalcuJumpCoe(deltaTime);
        }

        /// <summary>
        /// ジャンプ力の係数を計算する
        /// ジャンプ時に_jumpingCoefficientgが段々高くなることで上に上がる力が段々弱まる
        /// </summary>
        private void CalcuJumpCoe(float deltaTime)
        {
            if (!_isJumping) return;
            _jumpingCoefficientg -= deltaTime;
            if (_jumpingCoefficientg <= 0) _jumpingCoefficientg = 0;
        }

        /// <summary>
        /// 重力の係数を計算する
        /// ジャンプ時に_gravityCoefficientgが段々高くなることで下に下がる力が段々高くなる
        /// </summary>
        private void CalcuGravityCoe(float deltaTime)
        {
            if (_isJumping)
            {
                _gravityCoefficientg = 0;
                return;
            }
            else
            {
                _gravityCoefficientg += deltaTime;
            }

            if (_gravityCoefficientg >= 1) _gravityCoefficientg = 1;
        }

        /// <summary>
        /// 位置を上下の限界内におさめる
        /// </summary>
        private void NormalizePos()
        {
            NormalizeHorizontalPos();
            NormalizeVerticalPos();

            void NormalizeHorizontalPos()
            {
                if (transform.position.x < _horizontalMoveLimit.Min)
                {
                    transform.position = new Vector3(_horizontalMoveLimit.Min, transform.position.y, transform.position.z);
                }

                if (transform.position.x > _horizontalMoveLimit.Max)
                {
                    transform.position = new Vector3(_horizontalMoveLimit.Max, transform.position.y, transform.position.z);
                }
            }

            void NormalizeVerticalPos()
            {
                if (transform.position.y < _verticalMoveLimit.Min)
                {
                    transform.position = new Vector3(transform.position.x, _verticalMoveLimit.Min, transform.position.z);
                }
            
                if (transform.position.y > _verticalMoveLimit.Max)
                {
                    transform.position = new Vector3(transform.position.x, _verticalMoveLimit.Max, transform.position.z);
                }
            }
        }

        public void Pause(bool pause)
        {
            _pause = pause;
        }
    }
}
