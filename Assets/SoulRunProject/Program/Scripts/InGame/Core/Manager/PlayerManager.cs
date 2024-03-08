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

        private void Awake()
        {
            _playerInputUsers = GetComponents<IUsePlayerInput>();
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
    }
}