using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤーのインプットを管理する
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        private readonly ReactiveProperty<Vector2> _moveInput = new ReactiveProperty<Vector2>();
        private readonly BoolReactiveProperty _jumpInput = new BoolReactiveProperty();
        private readonly BoolReactiveProperty _pauseInput = new BoolReactiveProperty();
        private readonly BoolReactiveProperty _shiftInput = new BoolReactiveProperty();

        public ReactiveProperty<Vector2> MoveInput => _moveInput;
        public BoolReactiveProperty JumpInput => _jumpInput;
        public BoolReactiveProperty PauseInput => _pauseInput;
        public BoolReactiveProperty ShiftInput => _shiftInput;

        private void Awake()
        {
            _moveInput.AddTo(this);
            _jumpInput.AddTo(this);
            _pauseInput.AddTo(this);
            _shiftInput.AddTo(this);
        }

        private void Update()
        {
            _moveInput.Value = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            _jumpInput.Value = Input.GetButtonDown("Jump");
            _pauseInput.Value = Input.GetButtonDown("Cancel");
            _shiftInput.Value = Input.GetButton("Fire3");
        }
    }
}
