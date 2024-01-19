using UnityEngine;
using UnityEngine.EventSystems;

namespace SoulRun.InGame
{
    /// <summary> ボタンの表示状態を管理するボタンクラス </summary>
    public abstract class InputUIButtonBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        /// <summary> クリックされているかどうか </summary>
        protected bool IsPressed { get; private set; } = false;

        /// <summary> ポインターがUIの上にあるときのカスタム処理を提供する </summary>
        protected virtual void OnPointerDownEvent()
        {
        }

        /// <summary> ポインターがUIの上から離れたときのカスタム処理を提供する </summary>
        protected virtual void OnPointerUpEvent()
        {
        }

        /// <summary> ポインターがUIの上にあるときに呼ばれる </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
            OnPointerDownEvent();
        }

        /// <summary> ポインターがUIの上から離れるときに呼ばれる </summary>
        public void OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
            OnPointerUpEvent();
        }
    }
}