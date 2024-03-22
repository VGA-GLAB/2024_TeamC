using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [Serializable]
    public class FireMeteor : SoulSkillBase
    {
        [SerializeField] private ParticleSystem _meteorParticle;
        
        public override async void StartSoulSkill()
        {
            _meteorParticle.Play();
            await UniTask.Delay(System.TimeSpan.FromSeconds(_skillParameter.Duration));
            _meteorParticle.Stop();
        }
    }
}
