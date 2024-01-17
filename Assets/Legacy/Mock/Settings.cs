using SoulRun.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class Settings : AbstractSingleton<Settings>
    {
        public float Speed = 1f;
        public float UpdateSpeed = 1f;
        public float SlimeStopTime = 2f;
        public AudioSource seSource;
        public AudioClip deathSe;

        public void PlaySE()
        {
            AlphaExpAndScoreManager.Instance.AddScore(100);
            Settings.Instance.seSource.PlayOneShot(Settings.Instance.deathSe);
        }
    }
}
