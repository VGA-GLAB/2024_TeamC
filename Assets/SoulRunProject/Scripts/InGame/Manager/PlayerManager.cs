using System;
using System.Collections.Generic;
using System.Linq;
using SoulRunProject.InGame;
using SoulRunProject.SoulMixScene;
using UniRx;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// プレイヤーを管理するクラス
    /// </summary>
    public class PlayerManager : MonoBehaviour, IPausable
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private BaseStatus _baseStatus;
        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private HitDamageEffectManager _hitDamageEffectManager;

        [SerializeField, CustomLabel("ダメージを受けた時の速度減少量")] private float _decreaseSpeed;
        private IPlayerPausable[] _inGameTimes;
        private PlayerLevelManager _pLevelManager;
        private SkillManager _skillManager;
        private SoulSkillManager _soulSkillManager;
        private PlayerMovement _playerMovement;
        //private PlayerStatusManager _statusManager;
        private PlayerResourceContainer _resourceContainer;
        private FieldMover _fieldMover;
        public ReadOnlyReactiveProperty<float> CurrentHp => CurrentPlayerStatus.CurrentHpProperty;
        public PlayerResourceContainer ResourceContainer => _resourceContainer;
        //public PlayerStatusManager PlayerStatusManager => _statusManager;
        
        [CustomLabel("現在のプレイヤーのステータス")]　public PlayerStatus CurrentPlayerStatus;
        
        /// <summary>ダメージを無効化出来るかどうかの条件を格納するリスト</summary>
        public List<Func<bool>> IgnoreDamagePredicates { get; } = new();

        public event Action OnDead;

        private void Awake()
        {
            Register();
            _inGameTimes = GetComponents<IPlayerPausable>();
            _pLevelManager = GetComponent<PlayerLevelManager>();
            _skillManager = GetComponent<SkillManager>();
            _soulSkillManager = GetComponent<SoulSkillManager>();
            _playerMovement = GetComponent<PlayerMovement>();
            _resourceContainer = new();
            //_statusManager = new PlayerStatusManager(_baseStatus.Status);
            CurrentPlayerStatus = new PlayerStatus(_baseStatus.PlayerStatus);
            _fieldMover = FindObjectOfType<FieldMover>();
            InitializeInput();
            CurrentHp.Where(hp => hp == 0).Subscribe(_ => Death()).AddTo(this);
            if (_pLevelManager) _pLevelManager.CurrentPlayerStatus = CurrentPlayerStatus;
            if (_fieldMover) _fieldMover.CurrentPlayerStatus = CurrentPlayerStatus;
        }

        private void OnDestroy()
        {
            UnRegister();
        }

        /// <summary>
        /// 入力を受け付けるクラスに対して入力と紐づける
        /// </summary>
        private void InitializeInput()
        {
            _playerInput.MoveInput.Subscribe(input => _playerMovement.InputMove(input));
            _playerInput.MoveInput.Subscribe(input => _playerMovement.RotatePlayer(input));
            _playerInput.JumpInput.Where(x => x).Subscribe(_ => _playerMovement.Jump()).AddTo(this);
            _playerInput.ShiftInput.Where(x => x).Subscribe(_ => UseSoulSkill()).AddTo(this);
        }
        public void Register()
        {
            PauseManager.RegisterPausableObject(this);
        }

        public void UnRegister()
        {
            PauseManager.UnRegisterPausableObject(this);
        }

        /// <summary>
        /// Pauseの切替
        /// </summary>
        /// <param name="toPause"></param>
        public void Pause(bool toPause)
        {
            foreach (var inGameTime in _inGameTimes)
            {
                inGameTime.Pause(toPause);
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
        
        public void Damage(float damage)
        {
            foreach (var predicate in IgnoreDamagePredicates.Where(cond=> cond != null))
            {
                if (predicate())
                {
                    return;
                }
            }
            _fieldMover.DownSpeed(_decreaseSpeed);
            _playerCamera.DamageCam();
            CurrentPlayerStatus.CurrentHp -= Calculator.CalcDamage(damage, CurrentPlayerStatus.DefenceValue, 0, 1);
            
            // 白色点滅メソッド
            _hitDamageEffectManager.HitFadeBlinkWhite();
            CriAudioManager.Instance.PlaySE( "SE_Damage");
        }

        public void Heal(float value)
        {
            CurrentPlayerStatus.CurrentHp += value;
        }
        
        
        private void Death()
        {
            OnDead?.Invoke();
            Debug.Log("GameOver");
            //SwitchPause(true);
        }

        #region SoulSkill関連
        /// <summary>
        /// SoulSkillを使用する
        /// </summary>
        public void UseSoulSkill()
        {
            _soulSkillManager.UseSoulSkill();
        }
        
        public void SetSoulSkill(SoulSkillType soulSkillType)
        {
            _soulSkillManager.SetSoulSkill(soulSkillType);
        }
        
        public void AddSoul(float soul)
        {
            _soulSkillManager.AddSoul(soul);
        }

        #endregion
    }
}