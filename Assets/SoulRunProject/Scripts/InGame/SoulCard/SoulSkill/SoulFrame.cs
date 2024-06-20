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
    public class SoulFrame : SoulSkillBase , ISerializationCallbackReceiver
    {
        [SerializeField , CustomLabel("使用パーティクル")] private GameObject _particle;
        [SerializeField, CustomLabel("１発のダメージ")] private int _attackDamage;
        [SerializeField, CustomLabel("ノックバック")] private GiveKnockBack _giveKnockBack;
        private GameObject _particleInstance;
        private ParticleSystem _meteorParticle;
        private Transform _playerTransform;

        public override void StartSoulSkill()
        {
            _playerTransform = FindObjectOfType<PlayerManager>().transform;
            if (!_particleInstance)
            {
                _particleInstance =  Instantiate(_particle , _playerTransform);
                _particleInstance.OnParticleCollisionAsObservable().Subscribe(ParticleCollision).AddTo(_particleInstance);
                _meteorParticle = _particleInstance.GetComponent<ParticleSystem>();
            }
            _meteorParticle.Play();
            Observable.Timer(TimeSpan.FromSeconds(_duration))
                .Subscribe(_=> _meteorParticle.Stop()).AddTo(_particleInstance);
        }

        public override void UpdateSoulSkill(float deltaTime)
        {
            
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
            if (!_meteorParticle) return;
            if (isPause)
            {
                _meteorParticle.Pause();
            }
            else
            {
                _meteorParticle.Play();
            }
        }

        public void OnBeforeSerialize()
        {
            _particleInstance = null;
            _meteorParticle = null;
            _playerTransform = null;
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}
