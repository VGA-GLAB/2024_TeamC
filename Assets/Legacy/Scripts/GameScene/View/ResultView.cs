using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoulRun
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField] GameObject _resultPanel;
        [SerializeField] Button _restartButton;

        public Button RestartButton => _restartButton;
        private void Start ()
        {
            _resultPanel.SetActive(false);
        }

        public void ShowResultPanel()
        {
            _resultPanel.SetActive(true);
        }
    }
}
