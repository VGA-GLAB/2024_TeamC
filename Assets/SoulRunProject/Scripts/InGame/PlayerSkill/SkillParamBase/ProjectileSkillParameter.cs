using System;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// スキルのパラメーター
    /// </summary>
    [Serializable]
    public class ProjectileSkillParameter : ISkillParameter
    {
        [SerializeField, CustomLabel("次にこのスキルを使えるまでの時間")] float _coolTime;
        [SerializeField, CustomLabel("同時発射するオブジェクトの数")] int _amount;
        [SerializeField, CustomLabel("敵にヒットしたときに与えるダメージ")] float _attackDamage;
        [SerializeField, CustomLabel("スキルのオブジェクトの大きさ")] float _size;
        [SerializeField, CustomLabel("スキルオブジェクトの移動速度")] float _speed;
        [SerializeField, CustomLabel("敵を何体まで貫通するか")] int _penetration;
        [SerializeField, CustomLabel("弾のライフタイム")] float _lifeTime;
        [SerializeField, CustomLabel("与ノックバック")]　GiveKnockBack _knockBack;
        [SerializeField, CustomLabel("ヒット時のソウル増加量")] private float _getSoulValueOnHit;
        public float BaseCoolTime { get => _coolTime; set => _coolTime = value; }
        public float BaseLifeTime { get => _lifeTime; set => _lifeTime = value; }
        public int BaseAmount { get => _amount; set => _amount = value; }
        public float BaseAttackDamage { get => _attackDamage; set => _attackDamage = value; }
        public GiveKnockBack BaseKnockBack { get => _knockBack; set => _knockBack = value; }
        public float BaseSize { get => _size; set => _size = value; }
        public float BaseSpeed { get => _speed; set => _speed = value; }
        public int BasePenetration { get => _penetration; set => _penetration = value; }
        public float GetSoulValueOnHit { get => _getSoulValueOnHit; set => _getSoulValueOnHit = value; }
        
        private PlayerStatus _status;
        public void SetPlayerStatus(in PlayerStatus status)
        {
            _status = status;
        }

        public float AttackDamage => BaseAttackDamage + _status.AttackValue;
        public float CoolTime => BaseCoolTime * _status.CoolTimeReductionRate;
        public float Size => BaseSize * _status.SkillSizeUpRate;
        public int Amount => BaseAmount + _status.BulletAmountExtension;
        public float Speed => BaseSpeed * _status.BulletSpeedUpRate;
        public int Penetration => BasePenetration + _status.PenetrateAmountExtension;
        public float LifeTime => BaseLifeTime;
        public GiveKnockBack KnockBack => BaseKnockBack;
        public ISkillParameter Clone()
        {
            return new ProjectileSkillParameter(this);
        }

        ProjectileSkillParameter(ProjectileSkillParameter param)
        {
            _coolTime = param._coolTime;
            _amount = param._amount;
            _attackDamage = param._attackDamage;
            _size = param._size;
            _speed = param._speed;
            _penetration = param._penetration;
            _lifeTime = param._lifeTime;
            _knockBack = param._knockBack;
            _getSoulValueOnHit = param._getSoulValueOnHit;
            _status = param._status;
        }
        public ProjectileSkillParameter(){}
    }
}
