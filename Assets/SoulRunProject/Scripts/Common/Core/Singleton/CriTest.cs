using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject
{
    public class CriTest : MonoBehaviour
    {
        [SerializeField] private CriAudioManager.CueSheet cueSheet;
        [SerializeField] private string cueName;

        void Start()
        {
            CriAudioManager.Instance.PlaySE(cueSheet, cueName);
        }
    }
}