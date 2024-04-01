using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [Serializable]
    public class FireMeteor : SoulSkillBase
    {
        [SerializeField] private ParticleSystem _meteorParticle;
        private float _currentPlayTime;
        
        public override void StartSoulSkill()
        {
            _meteorParticle.Play();
        }

        public void Update()
        {
            if (_meteorParticle.isPlaying)
            {
                _currentPlayTime += Time.deltaTime;
                if (_currentPlayTime >= _skillParameterBase.LifeTime)
                {
                    _meteorParticle.Stop();
                    _currentPlayTime = 0;
                }
            }
        }

        public override void PauseSoulSkill(bool isPause)
        {
            if (isPause)
            {
                _meteorParticle.Pause();
            }
            else
            {
                _meteorParticle.Play();
            }
        }
    }
}
