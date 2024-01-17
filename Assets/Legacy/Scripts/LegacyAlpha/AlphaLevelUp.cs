using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRun.InGame
{
    public class AlphaLevelUp : MonoBehaviour
    {
        [SerializeField] Button _skillUp;
        [SerializeField] GameObject _ui;
        [SerializeField] GameObject _player;

        void Start ()
        {
            _skillUp.onClick.AddListener(() =>
            {
                AddSkill();
                AlphaExpAndScoreManager.Instance._paused = false;
                _ui.SetActive(false);
            });
            
        }

        public void AddSkill()
        {
            var skill = _player.GetComponent<AlphaPlayerAttack2>();
            skill.enabled = true;
        }
    }
}
