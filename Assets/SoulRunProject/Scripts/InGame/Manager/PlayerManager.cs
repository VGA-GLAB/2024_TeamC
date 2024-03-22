using System;
using Cysharp.Threading.Tasks;
using SoulRunProject.InGame;
using SoulRunProject.SoulMixScene;
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
        [SerializeField] private Status _status;
        
        private IInGameTime[] _inGameTimes;
        private PlayerLevelManager _pLevelManager;
        private SkillManager _skillManager;
        private SoulSkillManager _soulSkillManager;
        private PlayerMovement _playerMovement;
        public FloatReactiveProperty CurrentHp { get; private set; }
        public float MaxHp => _status.Hp;

        private void Awake()
        {
            _status = _status.Copy();
            CurrentHp = new FloatReactiveProperty(_status.Hp);
            _inGameTimes = GetComponents<IInGameTime>();
            _pLevelManager = GetComponent<PlayerLevelManager>();
            _skillManager = GetComponent<SkillManager>();
            _soulSkillManager = GetComponent<SoulSkillManager>();
            _playerMovement = GetComponent<PlayerMovement>();
            
            InitializeInput();
        }

        /// <summary>
        /// 入力を受け付けるクラスに対して入力と紐づける
        /// </summary>
        private void InitializeInput()
        {
            _playerInput.HorizontalInput.Subscribe(input => _playerMovement.InputHorizontal(input)).AddTo(this);
            _playerInput.JumpInput.Where(x => x).Subscribe(_ => _playerMovement.Jump()).AddTo(this);
            _playerInput.ShiftInput.Where(x => x).Subscribe(_ => UseSoulSkill()).AddTo(this);
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

        /// <summary>
        /// Skillを追加する
        /// </summary>
        /// <param name="skill"></param>
        public void AddSkill(SkillBase skill)
        {
            _skillManager.AddSkill(skill);
        }
        
        private void Death()
        {
            Debug.Log("GameOver");
            SwitchPause(true);
        }

        /// <summary>
        /// 仮の当たり判定関数
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out FieldEntityController fieldEntityController))
            {
                Damage(fieldEntityController.Status.Attack);
            }
        }

        #region SoulSkill関連
        /// <summary>
        /// SoulSkillを使用する
        /// </summary>
        public void UseSoulSkill()
        {
            _soulSkillManager.UseSoulSkill();
        }
        
        public void SetSoul(SoulSkillBase soul)
        {
            _soulSkillManager.SetSoulSkill(soul);
        }
        
        private void AddSoul(float soul)
        {
            _soulSkillManager.AddSoul(soul);
        }
        

        #endregion
    }
}