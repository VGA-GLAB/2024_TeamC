using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [Serializable]
    [CreateAssetMenu(fileName = "SoulFrame", menuName = "SoulRunProject/SoulSkill/SoulFrame")]
    public class SoulFrame : SoulSkillBase
    {
        [SerializeField , CustomLabel("使用パーティクル")] private GameObject _particle;
        [SerializeField, CustomLabel("１発のダメージ")] private int _attackDamage;
        [SerializeField, CustomLabel("発動時間")] private int _duration;
        [SerializeField, CustomLabel("ノックバック")] private GiveKnockBack _giveKnockBack;
        private float _currentPlayTime;
        private ParticleSystem _meteorParticle;
        private Transform _playerTransform;
        

        public override void StartSoulSkill()
        {
            _playerTransform = FindObjectOfType<PlayerManager>().transform;
            var particle =  Instantiate(_particle , _playerTransform);
            particle.OnParticleCollisionAsObservable().Subscribe(ParticleCollision).AddTo(particle);
            _meteorParticle = particle.GetComponent<ParticleSystem>();
            _meteorParticle.Play();
        }

        public override void UpdateSoulSkill(float deltaTime)
        {
            if (_meteorParticle.isPlaying)
            {
                _currentPlayTime += Time.deltaTime;
                if (_currentPlayTime >= _duration)
                {
                    _meteorParticle.Stop();
                    _currentPlayTime = 0;
                }
            }
        }
        
        private void ParticleCollision(GameObject other)
        {
            if (other.TryGetComponent(out DamageableEntity entity))
            {
                entity.Damage((int)_attackDamage , _giveKnockBack);
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
