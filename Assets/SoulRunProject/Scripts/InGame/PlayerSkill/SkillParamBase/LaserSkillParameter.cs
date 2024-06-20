using System;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable]
    public class LaserSkillParameter : ISkillParameter
    {
        [SerializeField, CustomLabel("1秒間あたりのダメージ量")] private float _damageOverTime = 10f;
        [SerializeField, CustomLabel("レーザーの数")] private int _amount = 1;
        [SerializeField, CustomLabel("薙ぎ払いの距離(横)")] private float _width = 10f;
        [SerializeField, CustomLabel("折り返しで奥に進む距離(奥)")] private float _height = 2f;
        [SerializeField, CustomLabel("薙ぎ払いのスピード")] private float _speed = 1f;
        [SerializeField, CustomLabel("折り返し回数")] private int _turnCount = 15;
        [SerializeField, CustomLabel("折り返しが全て終わった後何秒で再起動するか")] private float _coolTime = 3f;
        private PlayerStatus _status;

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public float Width
        {
            get => _width;
            set => _width = value;
        }

        public float Height
        {
            get => _height;
            set => _height = value;
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public int TurnCount
        {
            get => _turnCount;
            set => _turnCount = value;
        }

        public float CoolTime
        {
            get => _coolTime;
            set => _coolTime = value; }

        public float DamageOverTime
        {
            get => _damageOverTime;
            set => _damageOverTime = value;
        }

        public void SetPlayerStatus(in PlayerStatus status)
        {
            _status = status;
        }

        public ISkillParameter Clone()
        {
            return new LaserSkillParameter(this);
        }
        LaserSkillParameter(LaserSkillParameter param)
        {
            _amount = param._amount;
            _width = param._width;
            _height = param._height;
            _speed = param._speed;
            _turnCount = param._turnCount;
            _coolTime = param._coolTime;
            _damageOverTime = param._damageOverTime;
            _status = param._status;
        }
        public LaserSkillParameter(){}
    }
}