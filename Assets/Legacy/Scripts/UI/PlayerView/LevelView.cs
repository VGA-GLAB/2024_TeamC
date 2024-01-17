using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using UniRx;

namespace SoulRun.InGame
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] Text levelText;
        [Inject] PlayerExpManager playerExpManager;

        private void Start()
        {
            playerExpManager.CurrentLevelUP.Subscribe(level => levelText.text = $"LEVEL:{level}");
        }

    }
}
