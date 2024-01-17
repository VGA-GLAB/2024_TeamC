using UnityEngine;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace SoulRun.InGame
{
    /// <summary>
    /// PlayerのModelとViewをつなぐクラス
    /// </summary>
    public class PlayerPresenter : IStartable
    {
        //Model
        PlayerManager _playerManager;
        PlayerExpManager _playerExpManager;
        PlayerScoreManager _playerScoreManager;
        //View
        HPGageUI _playerHitPointView;
        ExpGageUI _expGageUI;
        ScoreView _palyerScoreView;

        public PlayerPresenter(PlayerManager playerManager, HPGageUI hitPointView, PlayerScoreManager playerScoreManager
            ,ScoreView scoreView, ExpGageUI expGageUI, PlayerExpManager playerExpManager)
        {
            _playerManager = playerManager;
            _playerHitPointView = hitPointView;
            _playerScoreManager = playerScoreManager;
            _palyerScoreView = scoreView;
            _expGageUI = expGageUI;
            _playerExpManager = playerExpManager;
        }

        public void Start()
        {
            SubscribeMethod();
        }

        /// <summary>
        /// 処理を登録する
        /// </summary>
        private void SubscribeMethod()
        {
            _playerHitPointView.SetGageUI(_playerManager.HitPoint.HP, _playerManager.HitPoint.HP);
            _playerManager.HitPoint.HPChanged  //体力のUI表示処理を登録
                .Subscribe(hp =>
                {
                    _playerHitPointView.SetCurrentGage(hp);
                }).AddTo(_playerManager);

            _playerScoreManager.Score   //スコアのUI表示処理を登録
                .Subscribe(score =>
                {
                    _palyerScoreView.SetScoreText(score);
                }).AddTo(_palyerScoreView);

            _expGageUI.SetGageUI(_playerExpManager.MaxExp, _playerExpManager.CurrentExp);
            _playerExpManager.CurrentExpChanged //ExpのUI表示処理を登録
                .Subscribe(exp => _expGageUI.SetCurrentGage(exp))
                .AddTo(_palyerScoreView);

            _playerExpManager.CurrentLevelMaxExp
                .Subscribe(maxExp => _expGageUI.SetMax(maxExp))
                .AddTo(_palyerScoreView);
        }
    }
}
