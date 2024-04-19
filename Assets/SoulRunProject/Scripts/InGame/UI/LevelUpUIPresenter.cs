using System.Collections.Generic;
using System.Linq;
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
            SkillBase selectedSkill = _skillManager.SkillData.Skills[Random.Range(0, _skillManager.SkillData.Skills.Count)];
            _levelUpView.UpgradeButtons[0].NameAndLevelText.text = selectedSkill.SkillName;

            if (_skillManager.CurrentSkillTypes.Contains(selectedSkill.SkillType)) // 取得済みスキルかによって分岐
            {
                _levelUpView.UpgradeButtons[0].InputUIButton.onClick.AsObservable()
                    .Subscribe(_ => _skillManager.LevelUpSkill(selectedSkill.SkillType)).AddTo(_disposableOnUpdateUI);
                _levelUpView.UpgradeButtons[0].NameAndLevelText.text += 
                    $"\nLv {_skillManager.CurrentSkill.FirstOrDefault(skillBase => skillBase.SkillType == selectedSkill.SkillType).CurrentLevel + 1}";
            }
            else
            {
                _levelUpView.UpgradeButtons[0].InputUIButton.onClick.AsObservable()
                    .Subscribe(_ => _skillManager.AddSkill(selectedSkill.SkillType)).AddTo(_disposableOnUpdateUI);
                _levelUpView.UpgradeButtons[0].NameAndLevelText.text += "\nLv 1";
            }

            _levelUpView.UpgradeButtons[0].ExplanatoryText.text = selectedSkill.ExplanatoryText;
            _levelUpView.UpgradeButtons[0].ButtonIconImage.sprite = selectedSkill.SkillIcon;
            
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
            _levelUpView.UpgradeButtons[1].NameAndLevelText.text = selectedStatusUpItems[0].ItemName;
            _levelUpView.UpgradeButtons[2].InputUIButton.onClick.AsObservable()
                .Subscribe(_ => selectedStatusUpItems[1].ItemEffect()).AddTo(_disposableOnUpdateUI);
            _levelUpView.UpgradeButtons[2].NameAndLevelText.text = selectedStatusUpItems[1].ItemName;
        }
    }
}
