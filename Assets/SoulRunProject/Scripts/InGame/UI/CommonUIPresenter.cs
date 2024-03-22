using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace SoulRunProject.InGame
{
    public class CommonUIPresenter : IStartable
    {
        private CommonView _view;
        PlayerManager _playerManager;
        PlayerLevelManager _playerLevelManager;
        SoulSkillManager _soulSkillManager;
        
        public CommonUIPresenter(CommonView view, PlayerManager playerManager, PlayerLevelManager playerLevelManager, SoulSkillManager soulSkillManager)
        {
            _view = view;
            _playerManager = playerManager;
            _playerLevelManager = playerLevelManager;
            _soulSkillManager = soulSkillManager;
        }

        public void Start()
        {
            _playerManager.CurrentHp.Subscribe(hp => _view.SetHpGauge(hp, _playerManager.MaxHp)).AddTo(_view);
            _playerLevelManager.OnCurrentExpChanged.Subscribe(exp => _view.SetExpGauge(exp, _playerLevelManager.CurrentMaxExp)).AddTo(_view);
            _playerLevelManager.OnCurrentLevelDataChanged.Subscribe(data => _view.SetLevelText(data.CurrentLevel)).AddTo(_view);
            _soulSkillManager.CurrentSoul?.Subscribe(current => _view.SetSoulGauge(current, _soulSkillManager.RequiredSoul)).AddTo(_view);
            //TODO: スキル、スコア、コインの表示を追加
            //playerManager.OnSkillIconChanged += (index, sprite) => _view.SetSkillIcon(index, sprite);
            //playerManager. += score => _view.SetScoreText(score);
            //playerManager.OnCoinChanged += coin => _view.SetCoinText(coin);
        }
    }
}
