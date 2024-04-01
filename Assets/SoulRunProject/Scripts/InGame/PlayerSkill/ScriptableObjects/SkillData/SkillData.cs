using System;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulRunProject
{
    /// <summary>
    /// Skillデータを作成する
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/SkillData")]
    public class SkillData : ScriptableObject
    {
        [SerializeReference, SubclassSelector, Header("スキル情報")] private SkillBase _skill;
        public SkillBase Skill => _skill;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += InitializeOnSceneLoaded;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= InitializeOnSceneLoaded;
        }

        /// <summary>
        /// シーンが切り替わる際にパラメータを初期化する
        /// </summary>
        private void InitializeOnSceneLoaded(Scene scene ,LoadSceneMode loadSceneMode)
        {
            Skill.InitializeParamOnSceneLoaded();
        }
    }
}
