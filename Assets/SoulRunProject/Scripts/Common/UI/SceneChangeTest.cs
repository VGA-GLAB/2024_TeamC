using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRun.InGame;
using UnityEngine;
using UniRx;

namespace SoulRunProject
{
    public class SceneChangeTest : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private InputUIButton _button;

        void Start()
        {
            _button.onClick.AddListener(_ => LoadingScene.Instance.LoadNextScene(_sceneName));
        }
    }
}