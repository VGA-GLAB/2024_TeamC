using SoulRunProject.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixSceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private SoulCardList _soulCardAllList;

        protected override void Configure(IContainerBuilder builder)
        {
            // ドメイン層
            builder.Register<SoulMixModel>(Lifetime.Scoped);
            builder.RegisterInstance(_soulCardAllList).AsSelf();
            
            // アプリケーション層
            builder.RegisterComponentInHierarchy<SoulCombiner>();
            builder.Register<SoulCardManager>(Lifetime.Singleton);
            
            // プレゼンテーション層
            builder.RegisterComponentInHierarchy<SoulMixView>();
            builder.Register<SoulMixPresenter>(Lifetime.Singleton);
            



            // 開始処理
            builder.RegisterEntryPoint<SoulCardManager>();
        }
    }
}