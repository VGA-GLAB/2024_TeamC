using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject
{
    public class CriTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            CriAudioManager.Instance.PlaySE(CriAudioManager.CueSheet.Bgm, "SUMMER_TRIANGLE");
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}