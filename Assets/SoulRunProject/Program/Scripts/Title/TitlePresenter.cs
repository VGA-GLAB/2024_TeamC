using System;
using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Title;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject
{
    public class TitlePresenter : MonoBehaviour
    {
        [SerializeField] private TitleModel _titleModel;
        [SerializeField] private TitleView _titleView;

        private void Start()
        {
            _titleView.StartButton.onClick.AsObservable().Subscribe(_ => _titleModel.StartGame());
            _titleView.OptionButton.onClick.AsObservable().Subscribe(_ => _titleModel.Option());
            _titleView.ExitButton.onClick.AsObservable().Subscribe(_ => _titleModel.Exit());
        }
    }
}
