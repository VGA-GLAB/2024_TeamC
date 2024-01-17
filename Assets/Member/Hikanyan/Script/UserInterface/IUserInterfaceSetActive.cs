using UniRx;
namespace SoulRun.InGame
{
    public interface IUserInterfaceSetActive
    {
        //UIの表示状態を管理するReactiveProperty
        ReactiveProperty<bool> IsVisible { get; }
        void SetActive(bool isActive);
    }
}