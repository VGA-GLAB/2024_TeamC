using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoulRun.InGame
{
    /// <summary>
    /// InGameの依存性解決を行うクラス
    /// </summary>
    public class InGameLifeTime : LifetimeScope
    {
        [SerializeField, Header("スキルを追加する")] private List<SkillBase> skillBases = new List<SkillBase>();
        [SerializeField] private HPGageUI hitPointView;
        [SerializeField] private ExpGageUI expGageUI;
        [SerializeField] private ScoreView scoreView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<InGameManager>();
            builder.RegisterComponentInHierarchy<PlayerManager>();
            builder.RegisterComponentInHierarchy<PlayerMover>();
            builder.RegisterComponentInHierarchy<GachaView>();
            builder.Register<PlayerCameraMover>(Lifetime.Singleton);
            builder.Register<PlayerExpManager>(Lifetime.Singleton);
            builder.Register<InGameManagerPresenter>(Lifetime.Singleton);
            builder.Register<PlayerScoreManager>(Lifetime.Singleton);
            builder.RegisterComponent(skillBases);
            builder.Register<PlayerSkillManager>(Lifetime.Singleton);
            builder.RegisterComponent(hitPointView);
            builder.RegisterComponent(expGageUI);
            builder.RegisterComponent(scoreView);
            builder.Register<PlayerPresenter>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PlayerPresenter>();
            builder.RegisterEntryPoint<InGameManagerPresenter>();
            builder.RegisterEntryPoint<PlayerCameraMover>();
        }
    }
}
