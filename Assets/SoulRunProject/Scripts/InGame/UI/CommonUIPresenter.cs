using SoulRunProject.Common;
using SoulRunProject.Framework;
using UniRx;
using VContainer.Unity;

namespace SoulRunProject.InGame
{
    public class CommonUIPresenter : IStartable
    {
        private CommonView _view;
        PlayerManager _playerManager;
        PlayerLevelManager _playerLevelManager;
        private SkillManager _skillManager;
        SoulSkillManager _soulSkillManager;
        
        public CommonUIPresenter(CommonView view, PlayerManager playerManager, PlayerLevelManager playerLevelManager,
            SkillManager skillManager, SoulSkillManager soulSkillManager)
        {
            _view = view;
            _playerManager = playerManager;
            _playerLevelManager = playerLevelManager;
            _skillManager = skillManager;
            _soulSkillManager = soulSkillManager;
        }

        public void Start()
        {
            _playerManager.CurrentHp.Subscribe(hp =>
            {
                DebugClass.Instance.ShowLog(hp.ToString());
                _view.SetHpGauge(hp, _playerManager.CurrentPlayerStatus.MaxHp);
            }).AddTo(_view);
            _playerLevelManager.OnCurrentExpChanged.Subscribe(exp => _view.SetExpGauge(exp, _playerLevelManager.CurrentExpToNextLevel)).AddTo(_view);
            _playerLevelManager.OnLevelUp.Subscribe(level => _view.SetLevelText(level)).AddTo(_view);
            _soulSkillManager.CurrentSoul?.Subscribe(current => _view.SetSoulGauge(current, _soulSkillManager.RequiredSoul)).AddTo(_view);
            //TODO: スキル
            _skillManager.OnAddSkill += skill => _view.SetSkillIcon(skill.SkillIcon, _skillManager.CreatedSkillList.Count - 1);
            //playerManager.OnSkillIconChanged += (index, sprite) => _view.SetSkillIcon(index, sprite);
            ScoreManager.Instance.OnScoreChanged.Subscribe(score => _view.SetScoreText(score))
                .AddTo(ScoreManager.Instance);
            Observable.EveryUpdate()
                .Select(_ => _playerManager.ResourceContainer.Coin)
                .Subscribe(coin =>
                {
                    _view.SetCoinText(coin);
                }).AddTo(_view);
            
        }
    }
}
