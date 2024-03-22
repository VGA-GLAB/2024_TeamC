using System;
using UniRx;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤーのレベルの管理
    /// </summary>
    public class PlayerLevelManager : MonoBehaviour
    {
        [SerializeField] private LevelUpTable _levelUpTable;
        
        /// <summary> 現在のレベルの情報 </summary>
        private readonly ReactiveProperty<LevelData> _currentLevelData = new ReactiveProperty<LevelData>(new LevelData());
        private readonly IntReactiveProperty _currentExp = new IntReactiveProperty(0);
        private int _levelDataIndex = 0;
        
        public IObservable<LevelData> OnCurrentLevelDataChanged => _currentLevelData;
        public IObservable<int> OnCurrentExpChanged => _currentExp;
        public int CurrentMaxExp => _currentLevelData.Value.ExpToNextLevel;

        private void Awake()
        {
            _currentLevelData.AddTo(this);
            _currentExp.AddTo(this);
            Initialize();
        }

        void Initialize()
        {
            // 設定されたレベルアップテーブルから初期化
            _currentLevelData.Value = _levelUpTable.Table[_levelDataIndex];
        }

        public void AddExp(int exp)
        {
            // 最大レベルのとき経験値取得処理をしない
            if (_levelUpTable.Table.Count <= _currentLevelData.Value.CurrentLevel)
            {
                return;
            }
            
            _currentExp.Value += exp;

            while (_currentExp.Value >= _currentLevelData.Value.ExpToNextLevel)
            {
                _currentExp.Value -= _currentLevelData.Value.ExpToNextLevel;
                LevelUp();
            }
        }

        void LevelUp()
        {
            _levelDataIndex++;
            _currentLevelData.Value = _levelUpTable.Table[_levelDataIndex];
        }
    }
}
