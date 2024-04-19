using System;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤーのレベルの管理
    /// </summary>
    public class PlayerLevelManager : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _initialLevel = 1;
        [SerializeField, EnumDrawer(typeof(SkillLevelLabel)), Min(1)] private int[] _expToNextLevel;

        private readonly IntReactiveProperty _currentLevel = new IntReactiveProperty();
        private readonly IntReactiveProperty _currentExp = new IntReactiveProperty(0);
        
        public IObservable<int> OnCurrentExpChanged => _currentExp;
        public IntReactiveProperty OnLevelUp => _currentLevel;
        public int CurrentExpToNextLevel => _expToNextLevel[_currentLevel.Value - 1];

        private void Awake()
        {
            _currentLevel.AddTo(this);
            _currentExp.AddTo(this);
            Initialize();
        }

        void Initialize()
        {
            // レベル初期化
            _currentLevel.Value = _initialLevel;
        }

        public void AddExp(int exp)
        {
            // 最大レベルのとき経験値取得処理をしない
            if (_expToNextLevel.Length <= _currentLevel.Value - 1)
            {
                return;
            }
            
            _currentExp.Value += exp;

            while (_currentExp.Value >= CurrentExpToNextLevel)
            {
                _currentExp.Value -= CurrentExpToNextLevel;
                LevelUp();
                
                if (_expToNextLevel.Length <= _currentLevel.Value - 1)
                {
                    return;
                }
            }
        }

        void LevelUp()
        {
            _currentLevel.Value++;
        }
    }
}
