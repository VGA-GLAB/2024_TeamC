using SoulRun.Core;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRun.InGame
{
    public class AlphaExpAndScoreManager : AbstractSingleton<AlphaExpAndScoreManager>
    {
        [SerializeField] float _currentExp = 0;
        [SerializeField] float _maxExp = 100;
        [SerializeField] Slider _slider;
        [SerializeField] float _level = 1;
        [SerializeField] float _score = 0;
        [SerializeField] Text _scoreText;
        [SerializeField] GameObject _levelUpPanel;
        private List<IAlphaPausable> _pausable;
        // Start is called before the first frame update
        public bool _paused = false; 


        void Start()
        {
            _levelUpPanel?.SetActive(false);
            _pausable = new();
            var player = FindObjectOfType<PlayerMover>();
            _pausable.Add((IAlphaPausable)player);
            var objs = FindObjectsByType<TestSlimeMove>(sortMode: FindObjectsSortMode.None);
            foreach ( var obj in objs )
            {
                _pausable.Add(obj.gameObject.GetComponent<IAlphaPausable>());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!_slider) return;
            _slider.value = _currentExp / _maxExp;
            _scoreText.text = $"SCORE:{(int)_score:D5}";

            if (_paused )
            {
                foreach (var obj in _pausable)
                {
                    obj.Pause(true);
                }
            }
            else
            {
                foreach (var obj in _pausable)
                {
                    obj.Pause(false);
                }
            }
        }

        public void AddExp(float exp)
        {
            _currentExp += exp;
         

            if ( _currentExp >= _maxExp )
            {   //レベルアップ処理
                _currentExp = 0;
                _level++;
                _levelUpPanel?.SetActive(true);
                Debug.Log(_pausable.Count);
                _paused = true;
            }
        }

        public void AddScore(float score)
        {
            _score += score;
        }
    }
    
    public class TestPause
    {
        public bool Paused = false;
    }

    public interface IAlphaPausable
    {
        void Pause(bool pause);
    }
}
