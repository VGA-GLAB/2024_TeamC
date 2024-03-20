using SoulRunProject.InGame;
using UniRx;
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
        private PlayerLevelManager _pLevelManager;

        private void Awake()
        {
            _playerInputUsers = GetComponents<IUsePlayerInput>();
            _inGameTimes = GetComponents<IInGameTime>();
            _pLevelManager = GetComponent<PlayerLevelManager>();
            
            InitializeInput();
        }

        /// <summary>
        /// 入力を受け付けるクラスに対して入力と紐づける
        /// </summary>
        private void InitializeInput()
        {
            foreach (var user in _playerInputUsers)
            {
                _playerInput.HorizontalInput.Subscribe(user.InputHorizontal);
                _playerInput.JumpInput.Where(x => x).Subscribe(_ => user.Jump());
            }
        }

        /// <summary>
        /// Pauseの切替
        /// </summary>
        /// <param name="toPause"></param>
        public void SwitchPause(bool toPause)
        {
            foreach (var inGameTime in _inGameTimes)
            {
                inGameTime.SwitchPause(toPause);
            }
        }

        /// <summary>
        /// 経験値を取得する
        /// </summary>
        /// <param name="exp">経験値量</param>
        public void GetExp(int exp)
        {
            _pLevelManager.AddExp(exp);
        }
    }
}