using System.Collections.Generic;
using SoulRunProject.Common;
using UniRx;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace SoulRunProject.InGame
{
    public class LevelUpUIPresenter : IInitializable
    {
        private PlayerManager _playerManager;
        private readonly LevelUpState _levelUpState;
        private readonly LevelUpItemData _levelUpItemData;
        private readonly LevelUpView _levelUpView;
        private SkillManager _skillManager;
        private CompositeDisposable _disposableOnUpdateUI = new();
        
        public LevelUpUIPresenter
            (PlayerManager playerManager, LevelUpState levelUpState, LevelUpItemData levelUpItemData,
                LevelUpView levelUpView, SkillManager skillManager)
        {
            _playerManager = playerManager;
            _levelUpState = levelUpState;
            _levelUpItemData = levelUpItemData;
            _levelUpView = levelUpView;
            _skillManager = skillManager;
        }

        public void Initialize()
        {
            // level up state の切替によってUIを切り替える
            _levelUpState.OnStateEnter += _ =>
            {
                _levelUpView.SetLevelUpPanelVisibility(true);
                UpdateUpgradeUI();
            };
            _levelUpState.OnStateExit += _ =>
            {
                _levelUpView.SetLevelUpPanelVisibility(false);
            };
            
            // upgradeされたら元のステートに戻る
            foreach (var upgradeButton in _levelUpView.UpgradeButtons)
            {
                upgradeButton.InputUIButton.onClick.AsObservable().Subscribe(_ => _levelUpState.SelectedSkill()).AddTo(_levelUpView);
            }

            _disposableOnUpdateUI.AddTo(_levelUpView);

            // アイテムに参照を渡す
            foreach (var skillLevelUpItem in _levelUpItemData.SkillLevelUpItems)
            {
                skillLevelUpItem.GetReference(_skillManager);
            }
            foreach (var statusUpItem in _levelUpItemData.StatusUpItems)
            {
                statusUpItem.GetReference(_playerManager);
            }
        }

        /// <summary>
        /// アップグレードのUIを更新する
        /// </summary>
        private void UpdateUpgradeUI()
        {
            _disposableOnUpdateUI.Clear();
            
            // ランダムにアイテムを選択し、ボタンに適用する
            // skill
            SkillLevelUpItem selectedSkillUpItem = _levelUpItemData.SkillLevelUpItems[Random.Range(0, _levelUpItemData.SkillLevelUpItems.Length)];
            _levelUpView.UpgradeButtons[0].InputUIButton.onClick.AsObservable()
                .Subscribe(_ => selectedSkillUpItem.ItemEffect()).AddTo(_disposableOnUpdateUI);
            _levelUpView.UpgradeButtons[0].ButtonText.text = selectedSkillUpItem.ItemName;
            // passive
            List<int> indexList = new();
            for (int i = 0; i < _levelUpItemData.StatusUpItems.Length; i++)
            {
                indexList.Add(i);
            }
            StatusUpItem[] selectedStatusUpItems = new StatusUpItem[2];
            for (int i = 0; i < 2; i++) // ランダムに２つ取得する
            {
                int index = Random.Range(0, indexList.Count);
                selectedStatusUpItems[i] = _levelUpItemData.StatusUpItems[indexList[index]];
                indexList.RemoveAt(index);
            }
            _levelUpView.UpgradeButtons[1].InputUIButton.onClick.AsObservable()
                .Subscribe(_ => selectedStatusUpItems[0].ItemEffect()).AddTo(_disposableOnUpdateUI);
            _levelUpView.UpgradeButtons[1].ButtonText.text = selectedStatusUpItems[0].ItemName;
            _levelUpView.UpgradeButtons[2].InputUIButton.onClick.AsObservable()
                .Subscribe(_ => selectedStatusUpItems[1].ItemEffect()).AddTo(_disposableOnUpdateUI);
            _levelUpView.UpgradeButtons[2].ButtonText.text = selectedStatusUpItems[1].ItemName;
        }
    }
}
