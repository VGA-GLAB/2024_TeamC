﻿using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SoulRun.InGame
{
    /// <summary> ボタンの表示状態を管理するボタンクラス View</summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class InputUIButton : InputUIButtonBase
    {
        private CanvasGroup _button;
        private Vector3 _originalScale;

        // イベントの定義
        public event Action OnButtonDown;
        public event Action OnButtonUp;
        // 自作のクリックイベントを定義
        [Serializable]
        public class ButtonClickEvent : UnityEvent<InputUIButton>
        {
        }

        public ButtonClickEvent onClick;
        
        private Subject<InputUIButton> _onClickSubject = new Subject<InputUIButton>();

        public IObservable<InputUIButton> OnClickAsObservable()
        {
            return _onClickSubject.AsObservable();
        }
        

        private void Start()
        {
            _button = GetComponent<CanvasGroup>();
            _originalScale = transform.localScale;
        }

        protected override void OnPointerDownEvent()
        {
            // DOTweenを使ってスケールを小さくするアニメーションを実行
            transform.DOScale(_originalScale * 0.8f, 0.2f);
            _button.alpha = 0.5f;
            // イベントの発火
            OnButtonDown?.Invoke();
        }

        protected override void OnPointerUpEvent()
        {
            // DOTweenを使ってスケールを元に戻すアニメーションを実行
            transform.DOScale(_originalScale, 0.2f);
            _button.alpha = 1f;
            // イベントの発火
            OnButtonUp?.Invoke();
            // クリックイベントの発火
            onClick?.Invoke(this);
        }
    }
}