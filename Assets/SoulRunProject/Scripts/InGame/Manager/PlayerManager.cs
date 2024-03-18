using System;
using SoulRunProject.Framework;
using SoulRunProject.InGame;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// プレイヤーを管理するクラス
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Status _status;
        private IUsePlayerInput[] _playerInputUsers;
        private IInGameTime[] _inGameTimes;
        private PlayerLevelManager _pLevelManager;
        private PlayerForwardMover _forwardMover;

        private void Awake()
        {
            _playerInputUsers = GetComponents<IUsePlayerInput>();
            _inGameTimes = GetComponents<IInGameTime>();
            _pLevelManager = GetComponent<PlayerLevelManager>();
            _forwardMover = GetComponent<PlayerForwardMover>();
            _status = _status.Copy();
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
        /// Playerの前進処理のPauseの切替
        /// </summary>
        /// <param name="isPause"></param>
        public void SetPlayerForwardMover(bool isPause)
        {
            _forwardMover.SwitchPause(isPause);
        }

        /// <summary>
        /// 経験値を取得する
        /// </summary>
        /// <param name="exp">経験値量</param>
        private void GetEXP(int exp)
        {
            _pLevelManager.AddExp(exp);
        }
        
        private void Damage(int damage)
        {
            _status.Hp -= damage;
            if (_status.Hp <= 0)
            {
                Death();
            }
        }
        
        private void Death()
        {
            // TODO 死亡時の処理を考える
            DebugClass.Instance.ShowLog("Player is Dead");
            SwitchPause(true);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out FieldEntityController fieldEntityController))
            {
                Damage(fieldEntityController.Status.Attack);
            }
        }
    }
}