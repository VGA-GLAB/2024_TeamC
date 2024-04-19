using System;
using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject.InGame
{
    public class SkillManager : MonoBehaviour, IPlayerPausable
    {
        [SerializeField , Header("スキルデータセット")] private SkillData _skillData;
        [SerializeField, HideInInspector] private Image[] _skillIconImage = new Image[5];
        private SkillData _skillDataCopy;
        private readonly List<SkillBase> _currentSkills = new(5);

        public SkillData SkillData => _skillData;
        public List<SkillBase> CurrentSkill => _currentSkills;
        /// <summary>
        /// 現在所持しているスキル名リスト
        /// </summary>
        public List<PlayerSkill> CurrentSkillTypes => _currentSkills.Select(x => x.SkillType).ToList();
        
        private bool _isPause;
        public void Start()
        {
            //Instantiateしないと、ScriptableObject内のクラスが生成されない。
            _skillDataCopy = Instantiate(_skillData);
            AddSkill(PlayerSkill.SoulBullet);
        }
        
        public void Update()
        {
            if (!_isPause)
            {
                foreach (var skill in _currentSkills)
                {
                    skill.UpdateSkill(Time.deltaTime);
                }
            }
        }
        
        /// <summary>
        /// スキルを追加する。追加時にStartSkill()を呼ぶ。
        /// </summary>
        /// <param name="skillType">スキル名</param>
        public void AddSkill(PlayerSkill skillType)
        {
            var skill = _skillDataCopy.Skills.FirstOrDefault(x => x.SkillType == skillType);
            if (skill != null)
            {
                _currentSkills.Add(skill);
                skill.StartSkill();
                _skillIconImage[_currentSkills.Count - 1].sprite = skill.SkillIcon; // スキルアイコンの表示
            }
            else
            {
                Debug.LogError("スキルリストに入っていないスキルがレベルアップ選択されました。");
            }
        }

        /// <summary>
        /// スキルのレベルアップ
        /// </summary>
        /// <param name="skillType">スキル名</param>
        public void LevelUpSkill(PlayerSkill skillType)
        {
            var skill = _currentSkills.FirstOrDefault(x => x.SkillType == skillType);
            if (skill != null)
            {
                skill.LevelUp();
            }
            else
            {
                Debug.LogError("スキルリストに入っていないスキルがレベルアップ選択されました。");
            }
        }
        
        public void Pause(bool isPause)
        {
            _isPause = isPause;
            foreach (var skill in _currentSkills)
            {
                skill.Pause(isPause);
            }
        }

        [CustomEditor(typeof(SkillManager))]
        public class SkillManagerEditor : Editor
        {
            private SkillManager _skillManager;
            private bool _groupIsOpen;
            
            private void Awake()
            {
                _skillManager = target as SkillManager;
            }

            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                _groupIsOpen = EditorGUILayout.BeginFoldoutHeaderGroup(_groupIsOpen, "スキルアイコン");

                if (_groupIsOpen)
                {
                    _skillManager._skillIconImage[0] = 
                        EditorGUILayout.ObjectField(_skillManager._skillIconImage[0], typeof(Image), true) as Image;
                    _skillManager._skillIconImage[1] = 
                        EditorGUILayout.ObjectField(_skillManager._skillIconImage[1], typeof(Image), true) as Image;
                    _skillManager._skillIconImage[2] = 
                        EditorGUILayout.ObjectField(_skillManager._skillIconImage[2], typeof(Image), true) as Image;
                    _skillManager._skillIconImage[3] = 
                        EditorGUILayout.ObjectField(_skillManager._skillIconImage[3], typeof(Image), true) as Image;
                    _skillManager._skillIconImage[4] = 
                        EditorGUILayout.ObjectField(_skillManager._skillIconImage[4], typeof(Image), true) as Image;
                }
                
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
        }
    }
}
