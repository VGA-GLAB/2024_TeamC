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
        private TitleModel _titleModel;
        private TitleView _titleView;
        private Button a;

        private void Start()
        {
            _titleView.StartButton.OnButtonDown += () => _titleModel.StartGame();
            // _titleView.OptionButton.OnClickAsObservable().Subscribe(_ => _titleModel.Option());
            // _titleView.ExitButton.OnClickAsObservable().Subscribe(_ => _titleModel.Exit());
        }
    }
}
