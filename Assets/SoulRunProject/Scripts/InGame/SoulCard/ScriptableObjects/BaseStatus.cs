using System;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    [CreateAssetMenu(fileName = "Status", menuName = "SoulRunProject/Status")]
    public class BaseStatus : ScriptableObject
    {
        [SerializeField] private PlayerStatus _playerStatus;
        public PlayerStatus PlayerStatus => _playerStatus;
    }

    [Serializable]
    public class PlayerStatus
    {
        [SerializeField , CustomLabel("HP") , Range( 0 , 1000)] private float _hp;
        [SerializeField, CustomLabel("攻撃力"), Range( 0 , 100)] private int _attackValue;
        [SerializeField, CustomLabel("防御力"), Range( 0 , 100)] private int _defenceValue;
        [SerializeField, CustomLabel("クールタイム減少率"), Range( 0 , 5)] private float _coolTimeReductionRate;
        [SerializeField, CustomLabel("スキル範囲増加率"), Range( 0 , 5)] private float _skillSizeUpRate;
        [SerializeField, CustomLabel("弾速増加率"), Range( 0 , 5)] private float _bulletSpeedUpRate;
        [SerializeField, CustomLabel("効果時間(秒)"), Range( 0 , 5)] private float _effectTimeExtension;
        [SerializeField, CustomLabel("追加弾数"), Range( 0 , 5)] private int _bulletAmountExtension;
        [SerializeField, CustomLabel("追加貫通力"), Range( 0 , 5)] private int _penetrateAmountExtension;
        [SerializeField, CustomLabel("初期移動スピード"), Range(0, 100)] private float _initialMoveSpeed;
        [SerializeField, CustomLabel("レベルアップ時の上昇スピード"), Range(0, 50)] private float _speedUpAtLevelUp;
        [SerializeField, CustomLabel("レベルアップ時の回復量"), Range(0, 100)] private float _healAtLevelUp;
        [SerializeField, CustomLabel("成長速度上昇率"), Range( 0 , 5)] private float _growthSpeedUpRate;
        [SerializeField, CustomLabel("ゴールド獲得量増加"), Range( 0 , 5)] private float _goldLuckRate;
        [SerializeField, CustomLabel("クリティカル率"), Range( 0 , 5)] private float _criticalRate;
        [SerializeField, CustomLabel("クリティカルダメージ倍率"), Range( 0 , 5)] private float _criticalDamageRate;
        [SerializeField, CustomLabel("アイテム吸収力範囲"), Range( 0 , 50)] private float _vacuumItemRangeRange;
        [SerializeField, CustomLabel("ドロップ率増加"), Range( 0 , 5)] private float _dropIncreasedRate;

        private FloatReactiveProperty _currentHp = new();
        public ReadOnlyReactiveProperty<float> CurrentHpProperty => _currentHp.ToReadOnlyReactiveProperty();

        public PlayerStatus(float hp, int attackValue, int defenceValue, float coolTimeReductionRate, float skillSizeUpRate,
            float bulletSpeedUpRate, float effectTimeExtension, int bulletAmountExtension, int penetrateAmountExtension, 
            float initialMoveSpeed, float speedUpAtLevelUp, float healAtLevelUp, float growthSpeedUpRate, float goldLuckRate,
            float criticalRate, float criticalDamageRate, float vacuumItemRangeRange, float dropIncreasedRate)
        {
            _hp = hp;
            _currentHp.Value = _hp;
            _attackValue = attackValue;
            _defenceValue = defenceValue;
            _coolTimeReductionRate = coolTimeReductionRate;
            _skillSizeUpRate = skillSizeUpRate;
            _bulletSpeedUpRate = bulletSpeedUpRate;
            _effectTimeExtension = effectTimeExtension;
            _bulletAmountExtension = bulletAmountExtension;
            _penetrateAmountExtension = penetrateAmountExtension;
            _initialMoveSpeed = initialMoveSpeed;
            _speedUpAtLevelUp = speedUpAtLevelUp;
            _healAtLevelUp = healAtLevelUp;
            _growthSpeedUpRate = growthSpeedUpRate;
            _goldLuckRate = goldLuckRate;
            _criticalRate = criticalRate;
            _criticalDamageRate = criticalDamageRate;
            _vacuumItemRangeRange = vacuumItemRangeRange;
            _dropIncreasedRate = dropIncreasedRate;
        }

        public PlayerStatus(PlayerStatus playerStatus)
        {
            _hp = playerStatus.MaxHp;
            _currentHp.Value = _hp;
            _attackValue = playerStatus.AttackValue;
            _defenceValue = playerStatus.DefenceValue;
            _coolTimeReductionRate = playerStatus.CoolTimeReductionRate;
            _skillSizeUpRate = playerStatus.SkillSizeUpRate;
            _bulletSpeedUpRate = playerStatus.BulletSpeedUpRate;
            _effectTimeExtension = playerStatus.EffectTimeExtension;
            _bulletAmountExtension = playerStatus.BulletAmountExtension;
            _penetrateAmountExtension = playerStatus.PenetrateAmountExtension;
            _initialMoveSpeed = playerStatus.MoveSpeed;
            _speedUpAtLevelUp = playerStatus.SpeedUpAtLevelUp;
            _healAtLevelUp = playerStatus.HealAtLevelUp;
            _growthSpeedUpRate = playerStatus.GrowthSpeedUpRate;
            _goldLuckRate = playerStatus.GoldLuckRate;
            _criticalRate = playerStatus.CriticalRate;
            _criticalDamageRate = playerStatus.CriticalDamageRate;
            _vacuumItemRangeRange = playerStatus.VacuumItemRange;
            _dropIncreasedRate = playerStatus.DropIncreasedRate;
        }

        /// <summary> 最大Hp </summary> 
        public float MaxHp
        {
            get => _hp;
            set
            {
                float diff = value - _hp;
                _hp = Mathf.Max(value, 0f); // HPは0未満にならないように制限
                
                if (diff > 0) // 最大hpが増えた場合は現在hpも同じだけ増える
                {
                    CurrentHp += diff;
                }
                else
                {
                    CurrentHp = _currentHp.Value; // 減った場合は最大値を超えないように更新する
                }
            }
        }

        /// <summary> 現在HP </summary>
        public float CurrentHp
        {
            get => _currentHp.Value;
            set => _currentHp.Value = Mathf.Clamp(value, 0, _hp);
        }

        /// <summary> 攻撃力 </summary> 
        public int AttackValue
        {
            get => _attackValue;
            set => _attackValue = value;
        }

        /// <summary> 防御力 </summary> 
        public int DefenceValue
        {
            get => _defenceValue;
            set => _defenceValue = value;
        }

        /// <summary> クールタイム減少率 </summary> 
        public float CoolTimeReductionRate
        {
            get => _coolTimeReductionRate;
            set => _coolTimeReductionRate = Mathf.Clamp(value, 0.00f, 5.00f); // 0.00から5.00までの範囲に制限
        }
        /// <summary> スキル範囲増加率 </summary> 
        public float SkillSizeUpRate
        {
            get => _skillSizeUpRate;
            set => _skillSizeUpRate = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> 弾速増加率 </summary> 
        public float BulletSpeedUpRate
        {
            get => _bulletSpeedUpRate;
            set => _bulletSpeedUpRate = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> 効果時間(秒) </summary> 
        public float EffectTimeExtension
        {
            get => _effectTimeExtension;
            set => _effectTimeExtension = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> 弾数 </summary> 
        public int BulletAmountExtension
        {
            get => _bulletAmountExtension;
            set => _bulletAmountExtension = Mathf.Max(value, 0); // 弾数は0未満にならないように制限
        }
        /// <summary> 貫通力 </summary> 
        public int PenetrateAmountExtension
        {
            get => _penetrateAmountExtension;
            set => _penetrateAmountExtension = Mathf.Max(value, 0); // 貫通力は0未満にならないように制限
        }
        /// <summary> 移動スピード </summary>
        public float MoveSpeed
        {
            get => _initialMoveSpeed;
            set => _initialMoveSpeed = Mathf.Max(value, 0);
        }
        /// <summary> レベルアップ時の上昇スピード </summary> 
        public float SpeedUpAtLevelUp
        {
            get => _speedUpAtLevelUp;
            set => _speedUpAtLevelUp = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> レベルアップ時の回復量 </summary>
        public float HealAtLevelUp
        {
            get => _healAtLevelUp;
            set => _healAtLevelUp = Mathf.Max(value, 0);
        }
        /// <summary> 成長速度 </summary> 
        public float GrowthSpeedUpRate
        {
            get => _growthSpeedUpRate;
            set => _growthSpeedUpRate = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> 運 </summary> 
        public float GoldLuckRate
        {
            get => _goldLuckRate;
            set => _goldLuckRate = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> クリティカル率 </summary> 
        public float CriticalRate
        {
            get => _criticalRate;
            set => _criticalRate = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> クリティカルダメージ倍率 </summary> 
        public float CriticalDamageRate
        {
            get => _criticalDamageRate;
            set => _criticalDamageRate = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> ソウル吸収力 </summary> 
        public float VacuumItemRange
        {
            get => _vacuumItemRangeRange;
            set => _vacuumItemRangeRange = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }

        /// <summary> ソウル獲得率 </summary> 
        public float DropIncreasedRate
        {
            get => _dropIncreasedRate;
            set => _dropIncreasedRate = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
    }
}