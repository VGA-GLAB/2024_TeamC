using System;
using SoulRunProject.Common;
using UnityEditor;
using UnityEngine;

namespace SoulRunProject.Skill
{
    /// <summary>
    /// 一定時間間隔で回復
    /// </summary>
    [CreateAssetMenu(menuName = "SoulRunProject/PlayerSkill/HealingSkill")]
    public class HealingSkill : SkillBase
    {
        [SerializeField, Tooltip("スキルエフェクト")] private ParticleSystem _healParticle;

        private HealingSkillParameter _healingSkillParam;
        private PlayerManager _playerManager;
        private float _coolTimer;
        
        HealingSkill()
        {
            _skillParam = new HealingSkillParameter();
            SkillLevelUpEvent = new SkillLevelUpEvent(new HealingSkillLevelUpEventListList());
        }

        public override void StartSkill()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            if (_skillParam is HealingSkillParameter param)
                _healingSkillParam = param;
            ActivateSkill();
            Instantiate(_healParticle, _playerManager.transform);
        }

        public override void UpdateSkill(float deltaTime)
        {
            if (_isPause) return;
            
            if (_coolTimer > 0)
            {
                _coolTimer -= deltaTime;
            }
            else if (_playerManager.CurrentHp.Value < _playerManager.MaxHp) // HPがMaxだと発動しない
            {
                ActivateSkill();
            }
        }

        /// <summary> スキル発動 </summary>
        void ActivateSkill()
        {
            _coolTimer = _healingSkillParam.CoolTime;
            _playerManager.Heal(_healingSkillParam.HealAmount);
            _healParticle.Play();
        }
        
        #if UNITY_EDITOR
        [CustomEditor(typeof(HealingSkill))]
        public class HealingSkillEditor : SkillBaseEditor
        {
            private HealingSkill _healingSkill;
            
            private void Awake()
            {
                _healingSkill = target as HealingSkill;
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                _healingSkill._healParticle = 
                    EditorGUILayout.ObjectField("エフェクト", _healingSkill._healParticle, typeof(ParticleSystem), true) as ParticleSystem;
            }
        }
        #endif
    }
}
