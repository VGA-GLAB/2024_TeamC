using System;
using UnityEngine;
using SoulRunProject.Common;
using UniRx;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 敵や障害物を管理するクラス
    /// </summary>
    public class DamageableEntity : MonoBehaviour
    {
        [SerializeField, CustomLabel("HP")] float _maxHp = 30;

        [SerializeField, CustomLabel("衝突ダメージ")]
        float _collisionDamage;

        [SerializeField, CustomLabel("ノックバック方向")]
        Vector3 _direction = Vector3.one;

        [SerializeField, CustomLabel("ノックバック処理")]
        TakeKnockBack _takeKnockBack;

        [SerializeField, CustomLabel("ドロップデータ")]
        LootTable _lootTable;

        [SerializeField, CustomLabel("ダメージエフェクト")]
        HitDamageEffectManager _hitDamageEffectManager;

        public Action OnDead;
        FloatReactiveProperty _currentHp = new();
        float _knockBackResistance;

        public float MaxHp => _maxHp;
        public float CollisionDamage => _collisionDamage;
        public FloatReactiveProperty CurrentHp => _currentHp;

        void Awake()
        {
            _currentHp.Value = _maxHp;
        }

        /// <summary>
        /// ダメージ処理 + ノックバック処理
        /// </summary>
        public void Damage(float damage, in GiveKnockBack knockBack = null)
        {
            _currentHp.Value -= damage;
            CriAudioManager.Instance.PlaySE(CriAudioManager.CueSheet.Se, "SE_Hit");
            if (_currentHp.Value <= 0)
            {
                Death();
            }

            if (knockBack != null)
            {
                _takeKnockBack.KnockBack(transform, knockBack.Power, _direction);
            }

            if (_hitDamageEffectManager)
            {
                _hitDamageEffectManager.HitFadeBlinkWhite();
            }
        }

        void Death()
        {
            if (_lootTable)
            {
                ItemDropManager.Instance.Drop(_lootTable, transform.position);
            }

            OnDead?.Invoke();
            Destroy(gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerManager playerManager))
            {
                playerManager.Damage(_collisionDamage);
            }
        }
    }
}