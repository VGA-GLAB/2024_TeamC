using System;
using System.Linq;
using SoulRunProject.Common;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public abstract class BulletBase : PooledObject, IPausable
    {
        [SerializeField] bool _useFirePointRotation;
        [SerializeField] bool _useFlash;
        [SerializeField] GameObject _hit;
        [SerializeField] GameObject _flash;
        [SerializeField] GameObject[] _detached;
        [SerializeField] Collider _collider;
        [SerializeField] Light _light;
        [SerializeField] private ParticleSystem _ps;
        [SerializeField] Vector3 _rotationOffset;
        [SerializeField] float _hitOffset;
        [SerializeField, CustomLabel("生存時間")] protected float _lifeTime = 15f;
        protected bool _isPause;
        protected PlayerManager PlayerManagerInstance;

        public void Awake()
        {
            if (_light != null)
                _light.enabled = false;
            _collider.enabled = false;
            _ps.Stop();
            Register();
        }

        new void OnDestroy()
        {
            base.OnDestroy();
            UnRegister();
        }
        public override void Initialize()
        {
            Fire();
        }

        public void GetReference(PlayerManager playerManager)
        {
            PlayerManagerInstance = playerManager;
        }

        public void Register()
        {
            PauseManager.RegisterPausableObject(this);
        }

        public void UnRegister()
        {
            PauseManager.UnRegisterPausableObject(this);
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
            if (isPause)
            {
                _ps.Pause();
            }
            else
            {
                _ps.Play();
            }
        }
        void Fire()
        {
            this.FixedUpdateAsObservable()
                .TakeUntilDisable(this)
                .TakeUntilDestroy(this)
                .Subscribe(_ => Move());
            Observable.Timer(TimeSpan.FromSeconds(_lifeTime))
                .TakeUntilDisable(this)
                .TakeUntilDestroy(this)
                .Subscribe(_ => Finish());
            
            if (_light != null)
                _light.enabled = true;
            _collider.enabled = true;
            _ps.Play();

            //  撃った時に出現するパーティクル
            if (_flash != null && _useFlash)
            {
                //Instantiate flash effect on projectile position
                var flashInstance = Instantiate(_flash, transform.position, Quaternion.identity);
                flashInstance.transform.forward = gameObject.transform.forward;

                //Destroy flash effect depending on particle Duration time
                var flashPs = flashInstance.GetComponent<ParticleSystem>();
                if (flashPs != null)
                {
                    Destroy(flashInstance, flashPs.main.duration);
                }
                else
                {
                    var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(flashInstance, flashPsParts.main.duration);
                }
            }
        }

        public abstract void Move();
        protected void OnHit(Collider other)
        {
            var closestPoint = other.ClosestPoint(transform.position);
            var normal = transform.position - closestPoint;
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, normal);
            Vector3 pos = closestPoint + normal * _hitOffset;
    
            //  当たった時に出るパーティクル
            if (_hit != null)
            {
                var hitInstance = Instantiate(_hit, pos, rot);
                if (_useFirePointRotation)
                {
                    hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0);
                }
                else if (_rotationOffset != Vector3.zero)
                {
                    hitInstance.transform.rotation = Quaternion.Euler(_rotationOffset);
                }
                else
                {
                    hitInstance.transform.LookAt(closestPoint + normal);
                }
    
                //Destroy hit effects depending on particle Duration time
                var hitPs = hitInstance.GetComponent<ParticleSystem>();
                if (hitPs != null)
                {
                    Destroy(hitInstance, hitPs.main.duration);
                }
                else
                {
                    var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitInstance, hitPsParts.main.duration);
                }
            }

            StartFinish();
        }

        void StartFinish()
        {
            _collider.enabled = false;
            if (_light != null)
                _light.enabled = false;
            _ps.Stop();
            _ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            if (_detached.Length > 0)
            {
                var detached = _detached.Where(prefab => prefab != null);
                foreach (var detachedPrefab in detached)
                {
                    detachedPrefab.transform.parent = null; //  オブジェクトを自身から分離する
                }
                Observable.Timer(TimeSpan.FromSeconds(1))
                    .TakeUntilDestroy(this)
                    .Subscribe(_ =>
                    {
                        foreach (var detachedPrefab in detached)
                        {
                            TouchCall(detachedPrefab);
                        }
                        Finish();
                    });
            }
            else
            {
                Finish();
            }
        }

        void TouchCall(GameObject detachedPrefab) //  弾が当たった時に一定時間待ってObjectPoolに入った自身のtransformに戻す
        {
            detachedPrefab.transform.SetParent(transform);
            detachedPrefab.transform.position = transform.position;
            detachedPrefab.transform.rotation = transform.rotation;
        }
    }
}
