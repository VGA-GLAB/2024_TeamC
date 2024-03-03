using System;
using System.Text;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame.PlayerSkill
{
    /// <summary>
    /// スキルのパラメーター
    /// </summary>
    [Serializable]
    public struct SkillParameter
    {
        [field: SerializeField, Tooltip("敵にヒットしたときに与えるダメージ")] 
        public float AttackDamage { get; private set; }
        [field: SerializeField, Tooltip("次にこのスキルを使えるまでの時間")] 
        public float CoolTime { get; private set; }
        [field: SerializeField, Tooltip("スキルのオブジェクトの大きさ")] 
        public float Range { get; private set; }
        [field: SerializeField, Tooltip("スキルオブジェクトの移動速度")] 
        public float Speed { get; private set; }
        [field: SerializeField,Tooltip("スキルの一回の発動時間")] 
        public float Duration { get; private set; }
        [field: SerializeField, Tooltip("スキルの一回の発射するオブジェクトの数")] 
        public int Amount { get; private set; }
        [field: SerializeField, Tooltip("敵を何体まで貫通するか")] 
        public int Penetration { get; private set; }
        [field: SerializeField, SerializeReference, SubclassSelector, Tooltip("独自パラメーター")]
        public IUniqueParameter UniqueParameter { get; private set; }
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
