using System;
using System.Text;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// スキルのパラメーター
    /// </summary>
    [Serializable]
    public struct SkillParameter
    {
        [SerializeField, Tooltip("敵にヒットしたときに与えるダメージ")] float _attackDamage;
        [SerializeField, Tooltip("次にこのスキルを使えるまでの時間")] float _coolTime;
        [SerializeField, Tooltip("スキルのオブジェクトの大きさ")] float _range;
        [SerializeField, Tooltip("スキルオブジェクトの移動速度")] float _speed;
        [SerializeField,Tooltip("スキルの一回の発動時間")] float _duration;
        [SerializeField, Tooltip("スキルの一回の発射するオブジェクトの数")] int _amount;
        [SerializeField, Tooltip("敵を何体まで貫通するか")] int _penetration;
        [SerializeReference, SubclassSelector, Tooltip("独自パラメーター")] 
        IUniqueParameter _uniqueParameter;
        public float AttackDamage => _attackDamage;
        public float CoolTime => _coolTime;
        public float Range => _range;
        public float Speed => _speed;
        /// <summary>スキルの一回の発動時間</summary>
        public float Duration => _duration;
        /// <summary>スキルの一回の発射するオブジェクトの数</summary>
        public int Amount => _amount;
        /// <summary>敵を何体まで貫通するか</summary>
        public int Penetration => _penetration;
        /// <summary>独自パラメーター</summary>
        public IUniqueParameter UniqueParameter => _uniqueParameter;
        /// <summary>
        /// デバッグ用。各パラメーターの情報を文字列で返す。
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append(nameof(AttackDamage)).Append(": ").Append(AttackDamage).AppendLine();
            sb.Append(nameof(CoolTime)).Append(": ").Append(CoolTime).AppendLine();
            sb.Append(nameof(Range)).Append(": ").Append(Range).AppendLine();
            sb.Append(nameof(Speed)).Append(": ").Append(Speed).AppendLine();
            sb.Append(nameof(Duration)).Append(": ").Append(Duration).AppendLine();
            sb.Append(nameof(Amount)).Append(": ").Append(Amount).AppendLine();
            sb.Append(nameof(Penetration)).Append(": ").Append(Penetration).AppendLine();
            return sb.ToString();
        }
    }
}
