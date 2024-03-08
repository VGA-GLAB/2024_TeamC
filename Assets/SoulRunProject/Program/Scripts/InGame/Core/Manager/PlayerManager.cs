using System;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// プレイヤーを管理するクラス
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        
        private IUsePlayerInput[] _playerInputUsers;
        private IInGameTime[] _inGameTimes;

        private bool _inPause;

        private void Awake()
        {
            _playerInputUsers = GetComponents<IUsePlayerInput>();
            _inGameTimes = GetComponents<IInGameTime>();
            InitializeInput();
        }

        /// <summary>
        /// 入力を受け付けるクラスに対して入力と紐づける
        /// </summary>
        private void InitializeInput()
        {
            foreach (var user in _playerInputUsers)
            {
                _playerInput.HorizontalAction += user.InputHorizontal;
                _playerInput.JumpAction += user.Jump;
            }
        }

        public void SwitchPause(bool toPause)
        {
            _inPause = toPause;

            foreach (var inGameTime in _inGameTimes)
            {
                inGameTime.SwitchPause(_inPause);
            }
        }

        private void Update()
        {
            if (_inPause)
            {
                return;
            }
            
            foreach (var inGameTime in _inGameTimes)
            {
                inGameTime.UpdateAction();
            }
        }

        private void FixedUpdate()
        {
            if (_inPause)
            {
                return;
            }

            foreach (var inGameTime in _inGameTimes)
            {
                inGameTime.FixedUpdateAction();
            }
        }
    }
}