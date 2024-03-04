using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerForwardMover playerForwardMover;
        protected override void Configure(IContainerBuilder builder)
        {
            //ドメイン層
            builder.RegisterInstance(_camera);
            builder.RegisterInstance(_playerMovement);
            builder.RegisterInstance(playerForwardMover);
            
            //アプリケーション層
            builder.Register<State, EnterInGameState>(Lifetime.Singleton);
            builder.Register<EnterStageState>(Lifetime.Singleton);
            builder.Register<PlayingRunGameState>(Lifetime.Singleton);
            builder.Register<GameOverState>(Lifetime.Singleton);
            builder.Register<EnterBossStageState>(Lifetime.Singleton);
            builder.Register<PlayingBossStageState>(Lifetime.Singleton);
            
        }
    }
}
