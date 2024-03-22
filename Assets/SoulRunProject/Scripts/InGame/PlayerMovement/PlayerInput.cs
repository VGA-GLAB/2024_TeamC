using System;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤーのインプットを管理する
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        private readonly FloatReactiveProperty _horizontalInput = new FloatReactiveProperty();
        private readonly BoolReactiveProperty _jumpInput = new BoolReactiveProperty();
        private readonly BoolReactiveProperty _pauseInput = new BoolReactiveProperty();

        public FloatReactiveProperty HorizontalInput => _horizontalInput;
        public BoolReactiveProperty JumpInput => _jumpInput;
        public BoolReactiveProperty PauseInput => _pauseInput;

        private void Awake()
        {
            _horizontalInput.AddTo(this);
            _jumpInput.AddTo(this);
            _pauseInput.AddTo(this);
        }

        private void Update()
        {
            _horizontalInput.Value = Input.GetAxisRaw("Horizontal");
            _jumpInput.Value = Input.GetButtonDown("Jump");
            _pauseInput.Value = Input.GetButtonDown("Cancel");
        }
    }
}
