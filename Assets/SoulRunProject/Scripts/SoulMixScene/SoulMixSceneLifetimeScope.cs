using System;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixSceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private SoulCardList _soulCardAllList; // ゲームに登場する全てのソウルカード
        [SerializeField] private List<SoulCombination> _soulCombinationList; // ソウルカードの組み合わせリスト

        protected void Start()
        {
            var soulCardManager = Container.Resolve<SoulCardManager>();
            var soulCombiner = Container.Resolve<SoulCombiner>();
            var soulMixPresenter = Container.Resolve<SoulMixPresenter>();
            soulCombiner.combinations = _soulCombinationList;
        }

        protected override void Configure(IContainerBuilder builder)
        {
            // ドメイン層
            builder.Register<SoulMixModel>(Lifetime.Scoped);
            builder.RegisterInstance(_soulCardAllList).AsSelf();
            builder.RegisterInstance(_soulCombinationList).AsSelf();

            // アプリケーション層
            builder.Register<SoulCombiner>(Lifetime.Singleton);
            builder.Register<SoulCardManager>(Lifetime.Singleton);

            // プレゼンテーション層
            builder.RegisterComponentInHierarchy<SoulMixView>();
            builder.Register<SoulMixPresenter>(Lifetime.Singleton).AsSelf();

            // 開始処理
            builder.RegisterEntryPoint<SoulMixPresenter>();
            builder.RegisterEntryPoint<SoulCardManager>();
        }
    }
}