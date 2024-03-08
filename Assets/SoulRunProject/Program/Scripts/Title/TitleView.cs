using System.Collections;
using System.Collections.Generic;
using SoulRun.InGame;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRunProject.Title
{
    /// <summary>
    /// タイトルの表示処理を行うクラス
    /// </summary>
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private InputUIButton _startButton;
        [SerializeField] private InputUIButton _optionButton;
        [SerializeField] private InputUIButton _exitButton;
        public InputUIButton StartButton => _startButton;
        public InputUIButton OptionButton => _optionButton;
        public InputUIButton ExitButton => _exitButton;
    }
}
