using SoulRunProject.InGame;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoulRunProject.Common
{
    /// <summary>
    /// インゲームの依存性注入を行うクラス
    /// </summary>
    public class InGameLifeTimeManager : LifetimeScope
    {
        [SerializeField] private PlayerCamera _camera;
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private PlayerLevelManager _playerLevelManager;
        [SerializeField] private SoulSkillManager _soulSkillManager;
        [SerializeField] private SkillManager _skillManager;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private ResultView _resultView;
        [SerializeField] private CommonView _commonView;
        [SerializeField] private StageNameView _stageNameView;
        [SerializeField] private LevelUpView _levelUpView;
        protected override void Configure(IContainerBuilder builder)
        {
            //ドメイン層
            builder.RegisterInstance(_camera);
            builder.RegisterInstance(_playerManager);
            builder.RegisterInstance(_playerInput);
            builder.RegisterInstance(gameObject);
            builder.RegisterInstance(_stageNameView);
            
            //アプリケーション層
            builder.Register<EnterInGameState>(Lifetime.Singleton);
            builder.Register<EnterStageState>(Lifetime.Singleton);
            builder.Register<PlayingRunGameState>(Lifetime.Singleton);
            builder.Register<GameOverState>(Lifetime.Singleton);
            builder.Register<EnterBossStageState>(Lifetime.Singleton);
            builder.Register<PlayingBossStageState>(Lifetime.Singleton);
            builder.Register<GameClearState>(Lifetime.Singleton);
            builder.Register<PauseState>(Lifetime.Singleton);
            builder.Register<LevelUpState>(Lifetime.Singleton);
            builder.Register<InGameManager>(Lifetime.Singleton);
            builder.RegisterInstance(_playerLevelManager);
            builder.RegisterInstance(_skillManager);
            builder.RegisterInstance(_soulSkillManager);
            
            //プレゼンテーション層
            builder.RegisterComponent(_resultView);
            builder.Register<ResultPresenter>(Lifetime.Singleton);
            builder.RegisterComponent(_commonView);
            builder.Register<CommonUIPresenter>(Lifetime.Singleton);
            builder.Register<StageEnterPresenter>(Lifetime.Singleton);
            builder.RegisterComponent(_levelUpView);
            builder.Register<LevelUpUIPresenter>(Lifetime.Singleton);
            
            //開始処理
            builder.RegisterEntryPoint<ResultPresenter>();
            builder.RegisterEntryPoint<StageEnterPresenter>();
            builder.RegisterEntryPoint<CommonUIPresenter>();
            builder.RegisterEntryPoint<LevelUpUIPresenter>();
            builder.RegisterEntryPoint<InGameManager>();
        }
    }
}
