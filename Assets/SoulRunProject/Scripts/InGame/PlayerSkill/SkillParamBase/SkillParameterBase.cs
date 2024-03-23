using System;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulRunProject.Common
{
    /// <summary>
    /// スキルのパラメーター
    /// </summary>
    [Serializable]
    public class SkillParameterBase : ScriptableObject
    {
        [SerializeField, Tooltip("次にこのスキルを使えるまでの時間")] float _coolTime;
        [SerializeField,Tooltip("スキルの持続時間")] float _lifeTime;
        
        /// <summary>
        /// ScriptableObjectのデータを上書きせずに、ランタイム時に変更したいためこのような書き方をしている。
        /// </summary>
        [NonSerialized] public float CoolTime;
        [NonSerialized] public float LifeTime;
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += RegisterDelegate;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= RegisterDelegate;
        }
        
        private void RegisterDelegate(Scene scene , LoadSceneMode loadSceneMode)
        {
            InitializeParam();
        }
        
        public virtual void InitializeParam()
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
