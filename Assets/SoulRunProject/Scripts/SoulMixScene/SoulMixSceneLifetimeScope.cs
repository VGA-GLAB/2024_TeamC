using VContainer;
using VContainer.Unity;

namespace SoulRunProject.SoulMixScene
{
    public class SoulMixSceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Presenter, View, Model, その他の依存関係の登録
            builder.RegisterComponentInHierarchy<SoulMixView>();
            builder.Register<SoulMixModel>(Lifetime.Scoped).AsSelf();
            builder.RegisterComponentInHierarchy<SoulMixPresenter>();

            // 他に必要な依存関係があればここで登録
        }
    }
}