using System.Collections;
using System.Collections.Generic;
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
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<State, AwakeInGameState>(Lifetime.Singleton);
            builder.Register<EnterStageState>(Lifetime.Singleton);
            builder.Register<PlayingRunGameState>(Lifetime.Singleton);
            builder.Register<GameOverState>(Lifetime.Singleton);
            builder.Register<EnterBossStageState>(Lifetime.Singleton);
            builder.Register<PlayingBossStageState>(Lifetime.Singleton);
        }
    }
}
