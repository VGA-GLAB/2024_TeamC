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
        private readonly BoolReactiveProperty _levelUpInput = new BoolReactiveProperty();

        public bool MoveInputActive { get; set; } = true;
        public bool JumpInputActive { get; set; } = true;
        public bool ShiftInputActive { get; set; } = true;
        public ReactiveProperty<Vector2> MoveInput => _moveInput;
        public BoolReactiveProperty JumpInput => _jumpInput;
        public BoolReactiveProperty PauseInput => _pauseInput;
        public BoolReactiveProperty ShiftInput => _shiftInput;
        public ReadOnlyReactiveProperty<bool> LevelUpInput => _levelUpInput.ToReadOnlyReactiveProperty();

        private void Awake()
        {
            _moveInput.AddTo(this);
            _jumpInput.AddTo(this);
            _pauseInput.AddTo(this);
            _shiftInput.AddTo(this);
            _levelUpInput.AddTo(this);
        }

        private void Update()
        {
            ReflectInput();
        }

        public void ReflectInput()
        {
            if (MoveInputActive) _moveInput.Value = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (JumpInputActive) _jumpInput.Value = Input.GetButtonDown("Jump");
            _pauseInput.Value = Input.GetButtonDown("Cancel");
            if (ShiftInputActive) _shiftInput.Value = Input.GetButton("Fire3");
            _levelUpInput.Value = Input.GetKeyDown(KeyCode.Q); // todo input manager
        }
    }
}
