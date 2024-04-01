using System;
using System.Text;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// スキルのパラメーター
    /// </summary>
    [Serializable , Name("基底クラス")]
    public abstract class SkillParameterBase
    {
        [SerializeField, Header("スキルの名前")] private PlayerSkill _skillType;
        [SerializeField, Header("次にこのスキルを使えるまでの時間")] float _coolTime;
        [SerializeField, Header("スキルの持続時間")] float _lifeTime;
        
        public PlayerSkill SkillType => _skillType;
        
        /// <summary>
        /// ScriptableObjectのデータを上書きせずに、ランタイム時に変更したいためこのような書き方をしている。
        /// </summary>
        [NonSerialized] public float CoolTime;
        [NonSerialized] public float LifeTime;
        
        /// <summary>
        /// シーンロード時にこのパラメータを初期化するように登録する。
        /// </summary>
        public virtual void InitializeParamOnSceneLoaded()
        {
            CoolTime = _coolTime;
            LifeTime = _lifeTime;
        }
        
        

        #region Debug用
        /// <summary>
        /// デバッグ用。各パラメーターの情報を文字列で返す。
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append(nameof(CoolTime)).Append(": ").Append(CoolTime).AppendLine();
            sb.Append(nameof(LifeTime)).Append(": ").Append(LifeTime).AppendLine();
            return sb.ToString();
        }
        #endregion

    }
}
