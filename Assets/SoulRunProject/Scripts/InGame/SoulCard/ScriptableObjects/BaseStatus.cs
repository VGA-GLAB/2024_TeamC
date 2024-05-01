using System;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    [CreateAssetMenu(fileName = "Status", menuName = "SoulRunProject/Status")]
    public class BaseStatus : ScriptableObject
    {
        [SerializeField] private Status _status;
        public Status Status => _status;
    }

    [Serializable]
    public class Status
    {
        [SerializeField , CustomLabel("HP") ] private float _hp;
        [SerializeField, CustomLabel("攻撃力")] private int _attack;
        [SerializeField, CustomLabel("防御力")] private int _defence;
        [SerializeField, CustomLabel("クールタイム減少率")] private float _coolTime;
        [SerializeField, CustomLabel("スキル範囲増加率")] private float _skillSize;
        [SerializeField, CustomLabel("弾速増加率")] private float _bulletSpeed;
        [SerializeField, CustomLabel("効果時間(秒)")] private float _effectTime;
        [SerializeField, CustomLabel("弾数")] private int _bulletNum;
        [SerializeField, CustomLabel("貫通力")] private float _penetration;
        [SerializeField, CustomLabel("移動スピード")] private float _moveSpeed;
        [SerializeField, CustomLabel("成長速度")] private float _growthSpeed;
        [SerializeField, CustomLabel("運")] private float _luck;
        [SerializeField, CustomLabel("クリティカル率")] private float _criticalRate;
        [SerializeField, CustomLabel("クリティカルダメージ倍率")] private float _criticalDamageRate;
        [SerializeField, CustomLabel("ソウル吸収力")] private float _soulAbsorption;
        [SerializeField, CustomLabel("ソウル獲得率")] private float _soulAcquisition;

        public Status(float hp, int attack, int defence, float coolTime, float skillSize, float bulletSpeed, float effectTime, int bulletNum, float penetration, float moveSpeed, float growthSpeed, float luck, float criticalRate, float criticalDamageRate, float soulAbsorption, float soulAcquisition)
        {
            _hp = hp;
            _attack = attack;
            _defence = defence;
            _coolTime = coolTime;
            _skillSize = skillSize;
            _bulletSpeed = bulletSpeed;
            _effectTime = effectTime;
            _bulletNum = bulletNum;
            _penetration = penetration;
            _moveSpeed = moveSpeed;
            _growthSpeed = growthSpeed;
            _luck = luck;
            _criticalRate = criticalRate;
            _criticalDamageRate = criticalDamageRate;
            _soulAbsorption = soulAbsorption;
            _soulAcquisition = soulAcquisition;
        }

        public Status(Status status)
        {
            _hp = status.Hp;
            _attack = status.Attack;
            _defence = status.Defence;
            _coolTime = status.CoolTime;
            _skillSize = status.SkillSize;
            _bulletSpeed = status.BulletSpeed;
            _effectTime = status.EffectTime;
            _bulletNum = status.BulletNum;
            _penetration = status.Penetration;
            _moveSpeed = status.MoveSpeed;
            _growthSpeed = status.GrowthSpeed;
            _luck = status.Luck;
            _criticalRate = status.CriticalRate;
            _criticalDamageRate = status.CriticalDamageRate;
            _soulAbsorption = status.SoulAbsorption;
            _soulAcquisition = status.SoulAcquisition;
        }

        /// <summary> Hp </summary> 
        public float Hp
        {
            get => _hp;
            set => _hp = Mathf.Max(value, 0f); // HPは0未満にならないように制限
        }

        /// <summary> 攻撃力 </summary> 
        public int Attack
        {
            get => _attack;
            set => _attack = value;
        }

        /// <summary> 防御力 </summary> 
        public int Defence
        {
            get => _defence;
            set => _defence = value;
        }

        /// <summary> クールタイム減少率 </summary> 
        public float CoolTime
        {
            get => _coolTime;
            set => _coolTime = Mathf.Clamp(value, 0.00f, 1.00f); // 0.00から1.00までの範囲に制限
        }
        /// <summary> スキル範囲増加率 </summary> 
        public float SkillSize
        {
            get => _skillSize;
            set => _skillSize = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> 弾速増加率 </summary> 
        public float BulletSpeed
        {
            get => _bulletSpeed;
            set => _bulletSpeed = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> 効果時間(秒) </summary> 
        public float EffectTime
        {
            get => _effectTime;
            set => _effectTime = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> 弾数 </summary> 
        public int BulletNum
        {
            get => _bulletNum;
            set => _bulletNum = Mathf.Max(value, 0); // 弾数は0未満にならないように制限
        }
        /// <summary> 貫通力 </summary> 
        public float Penetration
        {
            get => _penetration;
            set => _penetration = Mathf.Max(value, 0.00f); // 貫通力は0未満にならないように制限
        }
        /// <summary> 移動スピード </summary> 
        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> 成長速度 </summary> 
        public float GrowthSpeed
        {
            get => _growthSpeed;
            set => _growthSpeed = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
        /// <summary> 運 </summary> 
        public float Luck
        {
            get => _luck;
            set => _luck = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
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
        public float SoulAbsorption
        {
            get => _soulAbsorption;
            set => _soulAbsorption = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }

        /// <summary> ソウル獲得率 </summary> 
        public float SoulAcquisition
        {
            get => _soulAcquisition;
            set => _soulAcquisition = Mathf.Max(value, 0.00f); // 0.00未満にならないように制限
        }
    }
}