using System;
using SoulRunProject.InGame;
using SoulRunProject.SoulMixScene;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

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

        private void Awake()
        {
            _status = _status.Copy();
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
        
        public void Damage(int damage)
        {
            _status.Hp -= damage;
            if (_status.Hp <= 0)
            {
                Death();
            }
        }
        
        private void Death()
        {
            Debug.Log("GameOver");
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