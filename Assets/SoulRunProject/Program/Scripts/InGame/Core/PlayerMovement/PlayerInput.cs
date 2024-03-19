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

        public FloatReactiveProperty HorizontalInput => _horizontalInput;
        public BoolReactiveProperty JumpInput => _jumpInput;

        private void Awake()
        {
            _horizontalInput.AddTo(this);
            _jumpInput.AddTo(this);
        }

        private void Update()
        {
            _horizontalInput.Value = Input.GetAxisRaw("Horizontal");
            _jumpInput.Value = Input.GetButtonDown("Jump");
        }
    }
}
