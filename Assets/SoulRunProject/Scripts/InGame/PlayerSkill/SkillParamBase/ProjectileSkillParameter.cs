using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// スキルのパラメーター
    /// </summary>
    [Serializable]
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/ProjectionSkillParameter")]
    public class ProjectileSkillParameter : SkillParameterBase
    {
        [SerializeField, Header("同時発射するオブジェクトの数")] int _amount;
        //[SerializeField, Header("発射間隔")] float _fireInterval;
        [SerializeField, Header("敵にヒットしたときに与えるダメージ")] float _attackDamage;
        [SerializeField, Header("スキルのオブジェクトの大きさ")] float _range;
        [SerializeField, Header("スキルオブジェクトの移動速度")] float _speed;
        [SerializeField, Header("敵を何体まで貫通するか")] int _penetration;
        [SerializeReference, SubclassSelector, Header("独自パラメーター")] 
        List<IUniqueParameter> _uniqueParameters;

        /// <summary>
        /// ScriptableObjectのデータを上書きせずに、ランタイム時に変更したいためこのような書き方をしている。
        /// </summary>
        [NonSerialized] public int Amount;
        [NonSerialized] public float FireInterval;
        [NonSerialized] public float AttackDamage;
        [NonSerialized] public float Range;
        [NonSerialized] public float Speed;
        [NonSerialized] public int Penetration;
        [NonSerialized] public List<IUniqueParameter> UniqueParameters;

        public override void InitializeParam()
        {
            base.InitializeParam();
            Amount = _amount;
            AttackDamage = _attackDamage;
            Range = _range;
            Speed = _speed;
            Penetration = _penetration;
            //ディープコピー
            UniqueParameters = new(_uniqueParameters);
        }

        #region Debug用
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
            sb.Append(nameof(LifeTime)).Append(": ").Append(LifeTime).AppendLine();
            sb.Append(nameof(Amount)).Append(": ").Append(Amount).AppendLine();
            sb.Append(nameof(Penetration)).Append(": ").Append(Penetration).AppendLine();
            return sb.ToString();
        }

        #endregion

    }
}
