using System;
using System.Collections.Generic;
using System.Linq;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class SkillManager : MonoBehaviour
    {
        [SerializeField, Header("初期スキルリスト")] private List<PlayerSkill> _defaultPlayerSkills;
        [SerializeField, CustomLabel("初期のスキル保有可能数")] private int _initialNumberOfPossessions;
        [SerializeField, Header("スキルの保有可能数が増えるレベル")] private int[] _increaseSkillLevels;
        private readonly List<AbstractSkill> _createdSkillList = new();
        private List<AbstractSkillData> _skillData;
        public List<AbstractSkillData> SkillData => _skillData;
        public List<AbstractSkill> CreatedSkillList => _createdSkillList;
        /// <summary>
        /// 現在所持しているスキル名リスト
        /// </summary>
        public List<PlayerSkill> CurrentSkillTypes => _createdSkillList.Select(x => x.SkillType).ToList();
        public event Action<AbstractSkillData> OnAddSkill;
        public bool CanGetNewSkill => _increaseSkillLevels.Count(level => level <= _playerLevelManager.OnLevelUp.Value) 
            + _initialNumberOfPossessions > _createdSkillList.Count;
        
        private bool _isPause;
        
        private PlayerManager _playerManager;
        private PlayerLevelManager _playerLevelManager;
        
        public void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            _playerLevelManager = GetComponent<PlayerLevelManager>();
            
            if (MyRepository.Instance.TryGetDataList<AbstractSkillData>(out var dataSet))
            {
                _skillData = dataSet ;
            }

            foreach (var skill in _defaultPlayerSkills)
            {
                AddSkill(skill);
            }
        }
        
        public void Update()
        {
            if (!_isPause)
            {
                foreach (var skill in _createdSkillList)
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
            var skill = _skillData.FirstOrDefault(x => x.SkillType == skillType);
            if (skill != null)
            {
                var createdSkill = SkillFactory.CreateSkill(skillType, skill, _playerManager);
                if (createdSkill != null)
                {
                    _createdSkillList.Add(createdSkill);
                    createdSkill.StartSkill();
                    OnAddSkill?.Invoke(skill);
                }
            }
            else
            {
                Debug.LogError("スキルリストに入っていないスキルが追加選択されました。");
            }
        }

        /// <summary>
        /// スキルのレベルアップ
        /// </summary>
        /// <param name="skillType">スキル名</param>
        public void LevelUpSkill(PlayerSkill skillType)
        {
            var skill = _createdSkillList.FirstOrDefault(x => x.SkillType == skillType);
            if (skill != null)
            {
                skill.LevelUp();
            }
            else
            {
                Debug.LogError("スキルリストに入っていないスキルがレベルアップ選択されました。");
            }
        }
    }
}
