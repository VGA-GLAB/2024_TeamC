using System;
using UnityEngine;
using SoulRun.InGame;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary> ボタンの表示状態を管理するボタンクラス Presenter</summary>
[RequireComponent(typeof(InputUIButton))]
public class TargetChangeUIPresenter : MonoBehaviour
{
    [SerializeField] private GameObject targetPanel;//view
    
    private InputUIButton _inputUIButton;
    private IUserInterfaceSetActive _userInterfaceSetActive;

    private void Start()
    {
        TryGetComponent(out _inputUIButton);

        // _targetPanelからIUserInterfaceを取得
        _userInterfaceSetActive = targetPanel.GetComponent<IUserInterfaceSetActive>();
        if (_userInterfaceSetActive != null)
        {
            // IUserInterfaceをUIManagerに登録
            UIManager.Instance.RegisterPanel(_userInterfaceSetActive);
        }
        else
        {
            Debug.LogError("_targetPanel does not implement IUserInterface");
        }

        // InputUIButtonのイベントにメソッドを登録
        _inputUIButton.OnClick += HandleButtonUp;
    }

    private void HandleButtonUp()
    {
        // IUserInterfaceの表示状態を反転
        _userInterfaceSetActive.IsVisible.Value = !_userInterfaceSetActive.IsVisible.Value;
    }

    private void OnDestroy()
    {
        _inputUIButton.OnButtonUp -= HandleButtonUp;
    }
}