using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// プレイヤーの経験値を管理するクラス
    /// 各レベルの必要経験値のデータテーブルとレベルアップ機能を持つ
    /// </summary>
    public class PlayerExpManager
    {
        private Dictionary<int, int> _expTable = new Dictionary<int, int>() {
            { 1, 1 },
            { 2, 2 },
            { 3, 3 },
            { 4, 4 },
        };    //1レべからキーは開始する
        private IntReactiveProperty _currentLevel = new IntReactiveProperty(1);
        private readonly IntReactiveProperty _currentExp = new IntReactiveProperty(0);
        private readonly IntReactiveProperty _currentLevelMaxExp = new IntReactiveProperty(1);
        private InGameManager _gameManager;

        public int CurrentLevel => _currentLevel.Value;
        public IObservable<int> CurrentLevelUP => _currentLevel;
        public int CurrentExp => _currentExp.Value;
        public IReadOnlyReactiveProperty<int> CurrentExpChanged => _currentExp;
        public int MaxExp => _currentLevelMaxExp.Value;
        public IReadOnlyReactiveProperty<int> CurrentLevelMaxExp => _currentLevelMaxExp;

        public PlayerExpManager(InGameManager inGameManager)
        {
            _gameManager = inGameManager;
        }

        /// <summary>
        /// 経験値を加算します
        /// </summary>
        /// <param name="exp"></param>
        public void AddExp(int exp)
        {
            _currentExp.Value += exp;

            if (_currentExp.Value >= _currentLevelMaxExp.Value)
            {   //現在の経験値が現在のレベルのMAX経験値を超えたら
                LevelUp();
            }
        }

        /// <summary>
        /// レベルをあげます
        /// </summary>
        private void LevelUp()
        {
            var overflowExp = _currentLevelMaxExp.Value - _currentExp.Value;
            _gameManager.DoGahcaEvent();
            if (_expTable.ContainsKey(_currentLevel.Value + 1))
            {   //次のレベルのデータがあるなら
                _currentLevel.Value++;
                _currentExp.Value = 0 + overflowExp;
                _currentLevelMaxExp.Value = _expTable[_currentLevel.Value];
            }
        }
    }
}
