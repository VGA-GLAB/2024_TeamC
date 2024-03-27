using SoulRunProject.Common;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer.Unity;

namespace SoulRunProject.InGame
{
    public class LevelUpUIPresenter : IInitializable
    {
        private readonly LevelUpState _levelUpState;
        private readonly LevelUpView _levelUpView;
        private SkillManager _skillManager;
        private CompositeDisposable _disposableOnUpdateUI = new();
        
        public LevelUpUIPresenter(LevelUpState levelUpState, LevelUpView levelUpView, SkillManager skillManager)
        {
            _levelUpState = levelUpState;
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
            
            // upgradeされたらStateに送る
            foreach (var upgradeButton in _levelUpView.UpgradeButtons)
            {
                upgradeButton.InputUIButton.onClick.AsObservable().Subscribe(_ => _levelUpState.SelectedSkill()).AddTo(_levelUpView);
            }

            _disposableOnUpdateUI.AddTo(_levelUpView);
        }

        /// <summary>
        /// アップグレードのUIを更新する
        /// </summary>
        private void UpdateUpgradeUI()
        {
            _disposableOnUpdateUI.Clear();
            
            // skillの表示を設定する
            // skillをランダムに選択
            PlayerSkill selectedSkill = (PlayerSkill)Random.Range(0, System.Enum.GetValues(typeof(PlayerSkill)).Length);

            if (_skillManager.CurrentSkillTypes.Contains(selectedSkill)) // そのスキルを持っている
            {
                _levelUpView.UpgradeButtons[0].InputUIButton.onClick.AsObservable()
                    .Subscribe(_ =>
                    {
                        _skillManager.LevelUpSkill(selectedSkill);
                    })
                    .AddTo(_disposableOnUpdateUI);
                _levelUpView.UpgradeButtons[0].ButtonText.text = selectedSkill.ToString(); // とりあえずスキル名表示
            }
            else // 持っていない
            {
                _levelUpView.UpgradeButtons[0].InputUIButton.onClick.AsObservable()
                    .Subscribe(_ =>
                    {
                        _skillManager.AddSkill(selectedSkill);
                    })
                    .AddTo(_disposableOnUpdateUI);
                _levelUpView.UpgradeButtons[0].ButtonText.text = selectedSkill.ToString(); // とりあえずスキル名表示
            }
        }
    }
}
