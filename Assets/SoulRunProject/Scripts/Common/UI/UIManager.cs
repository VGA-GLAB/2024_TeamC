using System;
using System.Collections.Generic;

using SoulRun.InGame;
using SoulRunProject.Common;
using UniRx;
using UnityEngine;

namespace SoulRunProject.SoulMixScene
{
    public class UIManager : AbstractSingletonMonoBehaviour<UIManager>
    {
        protected override bool UseDontDestroyOnLoad => true;
        private readonly Dictionary<Type, IUserInterfaceSetActive> _panels = new ();
        

        public void RegisterPanel(IUserInterfaceSetActive panel)
        {
            Type panelType = panel.GetType();
            _panels[panelType] = panel;

            CompositeDisposable compositeDisposable = new CompositeDisposable();
            panel.IsVisible.Subscribe(isVisible =>
            {
                SetPanelVisibility(panel, isVisible);
            }).AddTo(compositeDisposable);
        }

        public void SetPanel(bool isVisible, IUserInterfaceSetActive panel)
        {
            SetPanelVisibility(panel, isVisible);
        }
        
        // 新しいSetPanelVisibilityメソッド
        private void SetPanelVisibility(IUserInterfaceSetActive panel, bool isVisible)
        {
            Type panelType = panel.GetType();
            if (_panels.TryGetValue(panelType, out IUserInterfaceSetActive registeredPanel))
            {
                registeredPanel.SetActive(isVisible);
            }
            else
            {
                Debug.LogWarning($"Panel of type {panelType} not found.");
            }
        }
        
        // ソウルの情報を表示するためのメソッド
        public void UpdateSoulDisplay(SoulCard soulCard)
        {
            //TODO: ここでSoulMixPanelを取得して、そこにソウルの情報を表示する
            //soulMixPanel.DisplayNewSoulInfo(soul);
        }

    }
}