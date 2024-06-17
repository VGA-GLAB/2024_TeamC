using System;
using UnityEngine;
using SoulRunProject.Common;
using SoulRunProject.Runtime;
using UniRx;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 敵や障害物を管理するクラス
    /// </summary>
    public class DamageableEntity : PooledObject
    {
        [SerializeField, CustomLabel("HP")] float _maxHp = 30;

        [SerializeField, CustomLabel("衝突ダメージ")]
        float _collisionDamage;

        [SerializeField, CustomLabel("ノックバックするかどうか")]
        private bool _canKnockback = true;

        [SerializeField, CustomLabel("ノックバック方向"), ShowWhenBoolean(nameof(_canKnockback))]
        Vector3 _direction = Vector3.one;

        [SerializeField, CustomLabel("ノックバック処理"), ShowWhenBoolean(nameof(_canKnockback))]
        TakeKnockBack _takeKnockBack;

        [SerializeField, CustomLabel("ドロップデータ")]
        LootTable _lootTable;

        [SerializeField, CustomLabel("ダメージエフェクト")]
        HitDamageEffectManager _hitDamageEffectManager;

        FloatReactiveProperty _currentHp = new();
        float _knockBackResistance;
        public Action<float> OnDamaged;
        public Action OnDead;
        public float MaxHp => _maxHp;
        public float CollisionDamage => _collisionDamage;
        public FloatReactiveProperty CurrentHp => _currentHp;
        private PlayerManager _player;
        private EnemyController _enemyController;

        void Start()
        {
            _player = FindObjectOfType<PlayerManager>();
            _enemyController = GetComponent<EnemyController>();
            Initialize();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!gameObject.activeSelf) return;
            if (other.gameObject.TryGetComponent(out PlayerManager playerManager))
                playerManager.Damage(_collisionDamage);
        }

        public override void Initialize()
        {
            _currentHp.Value = _maxHp;
            if (_enemyController) _enemyController.Initialize();
        }

        /// <summary>
        /// ダメージ処理 + ノックバック処理
        /// </summary>
        public void Damage(float damage, in GiveKnockBack knockBack = null, bool useSE = true)
        {
            if (!gameObject.activeSelf) return;
            if (!_player) return;
            float calculatedDamage = Calculator.CalcDamage(damage, 0, _player.CurrentPlayerStatus.CriticalRate, _player.CurrentPlayerStatus.CriticalDamageRate);
            _currentHp.Value -= calculatedDamage;
            OnDamaged?.Invoke(calculatedDamage);

            if (useSE) CriAudioManager.Instance.PlaySE("SE_Hit");

            if (_currentHp.Value <= 0)
            {
                Death();
            }

            if (knockBack != null && _canKnockback)
            {
                _takeKnockBack.KnockBack(transform, knockBack.Power, _direction);
            }

            if (_hitDamageEffectManager)
            {
                _hitDamageEffectManager.HitFadeBlinkWhite();
            }
        }

        public override void OnFinish()
        {
            OnDead = null;
        }

        public void Death()
        {
            if (_lootTable)
            {
                DropManager.Instance.RequestDrop(_lootTable, transform.position);
            }

            OnDead?.Invoke();
            Finish();
        }

        public void Despawn()
        {
            Debug.Log($"{gameObject.name} Despawn!");
            Finish();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!gameObject.activeSelf) return;
            if (other.gameObject.TryGetComponent(out PlayerManager playerManager))
            {
                playerManager.Damage(_collisionDamage);
            }
        }
    }
}