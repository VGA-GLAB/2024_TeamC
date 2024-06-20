using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using UniRx;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace SoulRunProject.InGame
{
    public class LevelUpUIPresenter : IInitializable
    {
        private readonly PlayerManager _playerManager;
        private readonly LevelUpState _levelUpState;
        private readonly LevelUpItemData _levelUpItemData;
        private readonly LevelUpView _levelUpView;
        private readonly SkillManager _skillManager;
        private readonly PlayerLevelManager _levelManager;
        private readonly CompositeDisposable _disposableOnUpdateUI = new();
        
        public LevelUpUIPresenter
            (PlayerManager playerManager, LevelUpState levelUpState, LevelUpItemData levelUpItemData,
                LevelUpView levelUpView, SkillManager skillManager, PlayerLevelManager levelManager)
        {
            _playerManager = playerManager;
            _levelUpState = levelUpState;
            _levelUpItemData = levelUpItemData;
            _levelUpView = levelUpView;
            _skillManager = skillManager;
            _levelManager = levelManager;
        }

        public void Initialize()
        {
            // level up state の切替によってUIを切り替える
            _levelUpState.OnStateEnter += _ =>
            {
                _levelUpView.OpenLevelUpPanel();
                UpdateUpgradeUI();
            };
            _levelUpState.OnStateExit += _ =>
            {
                _levelUpView.CloseLevelUpPanel().Forget();
            };
            
            // upgradeされたら元のステートに戻る
            // foreach (var upgradeButton in _levelUpView.UpgradeButtons)
            // {
            //     upgradeButton.InputUIButton.onClick.AsObservable().Subscribe(_ => _levelUpState.EndSelectSkill()).AddTo(_levelUpView);
            // }

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
            AbstractSkillData selectedSkillData;

            if (_skillManager.CanGetNewSkill) // 新しいスキル
            {
                AbstractSkillData[] notCreatedSkills = _skillManager.SkillData
                    .Where(skillBase => !_skillManager.CreatedSkillList.Select(skill=>skill.AbstractSkillData).Contains(skillBase)).ToArray();
                selectedSkillData = notCreatedSkills[Random.Range(0, notCreatedSkills.Length)];
                
                _levelUpView.UpgradeButtons[0].InputUIButton.onClick.AsObservable()
                    .Subscribe( _ =>
                    {
                        _skillManager.AddSkill(selectedSkillData.SkillType);
                        _levelUpState.EndSelectSkill();
                    }).AddTo(_disposableOnUpdateUI);
                _levelUpView.UpgradeButtons[0].NameAndLevelText.text = $"{selectedSkillData.SkillName}\nLv 1";
            }
            else // 所持スキルのレベルアップ
            {
                var createdSkill = _skillManager.CreatedSkillList[Random.Range(0, _skillManager.CreatedSkillList.Count)];
                selectedSkillData = createdSkill.AbstractSkillData;
                
                _levelUpView.UpgradeButtons[0].InputUIButton.onClick.AsObservable()
                    .Subscribe(_ =>
                    {
                        _skillManager.LevelUpSkill(selectedSkillData.SkillType);
                        _levelUpState.EndSelectSkill();
                    }).AddTo(_disposableOnUpdateUI);
                _levelUpView.UpgradeButtons[0].NameAndLevelText.text = 
                    $"{selectedSkillData.SkillName}\nLv {createdSkill.CurrentLevel + 1}";
            }

            _levelUpView.UpgradeButtons[0].ExplanatoryText.text = selectedSkillData.ExplanatoryText;
            _levelUpView.UpgradeButtons[0].ButtonIconImage.sprite = selectedSkillData.SkillIcon;
            
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
                .Subscribe(_ =>
                {
                    selectedStatusUpItems[0].ItemEffect();
                    _levelUpState.EndSelectSkill();
                }).AddTo(_disposableOnUpdateUI);
            _levelUpView.UpgradeButtons[1].NameAndLevelText.text = selectedStatusUpItems[0].ItemName;
            _levelUpView.UpgradeButtons[1].ExplanatoryText.text = selectedStatusUpItems[0].ExplanatoryText;
            _levelUpView.UpgradeButtons[1].ButtonIconImage.sprite = selectedStatusUpItems[0].ItemIcon;
            _levelUpView.UpgradeButtons[2].InputUIButton.onClick.AsObservable()
                .Subscribe(_ =>
                {
                    selectedStatusUpItems[1].ItemEffect();
                    _levelUpState.EndSelectSkill();
                }).AddTo(_disposableOnUpdateUI);
            _levelUpView.UpgradeButtons[2].NameAndLevelText.text = selectedStatusUpItems[1].ItemName;
            _levelUpView.UpgradeButtons[2].ExplanatoryText.text = selectedStatusUpItems[1].ExplanatoryText;
            _levelUpView.UpgradeButtons[2].ButtonIconImage.sprite = selectedStatusUpItems[1].ItemIcon;
        }
    }
}
