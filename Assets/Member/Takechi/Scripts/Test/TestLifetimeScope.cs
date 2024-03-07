using SoulRunProject.InGame;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SoulRunProject.TakechiTest
{
    /// <summary>
    /// デバッグ用LifetimeScope
    /// </summary>
    public class TestLifetimeScope : LifetimeScope
    {
        [SerializeField] TestPlayerManager _testPlayerManager;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent<IPlayerReference>(_testPlayerManager);
        }
    }
}