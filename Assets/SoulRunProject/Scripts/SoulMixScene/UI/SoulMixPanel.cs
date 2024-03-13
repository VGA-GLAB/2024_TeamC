using UniRx;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    /// <summary> ボタンの表示状態を管理するReactivePropertyを持つボタンクラス </summary>
    public class SoulMixPanel : MonoBehaviour, IUserInterfaceSetActive
    {
        // UIの表示状態を管理するReactiveProperty
        public ReactiveProperty<bool> IsVisible { get; private set; } = new(false);
        void Awake()
        {
            // GameObjectの現在のアクティブ状態に基づいてReactivePropertyを初期化
            IsVisible = new ReactiveProperty<bool>(this.gameObject.activeSelf);
        }
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}