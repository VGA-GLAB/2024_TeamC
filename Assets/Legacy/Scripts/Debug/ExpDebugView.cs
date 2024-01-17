using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace SoulRun.InGame
{
    public class ExpDebugView : MonoBehaviour
    {
        [Inject] private PlayerExpManager playerExpManager;
        [SerializeField] private Text expText;

        private void Update()
        {
            if (playerExpManager != null && expText != null)
                expText.text = $"EXP:{playerExpManager.CurrentExp}/{playerExpManager.MaxExp}";
        }
    }
}
