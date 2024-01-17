using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// プレイヤーから発射されるソウルバレットを動かすクラス
    /// </summary>
    public class SoulBulletObject : MonoBehaviour, IPausable
    {
        [SerializeField] private float _speed = 1.0f;
        private float _restAliveTime = 1;
        public event Action OnDeath;
        private bool _isPose = false;
        public Vector3 dir = Vector3.forward;

        public void SetBulletStatus(float speed, float time)
        {
            _restAliveTime = time;
            _speed = speed;
        }

        private void Update()
        {
            if (_isPose) return;
            transform.position += dir * _speed * Time.deltaTime;
            CountAliveTime();
        }

        private void CountAliveTime()
        {
            _restAliveTime -= Time.deltaTime;
            if (_restAliveTime <= 0.0f) OnDeath?.Invoke();
        }

        public void Active()
        {
            _isPose = false;
        }

        public void Pause()
        {
            _isPose = true;
        }
    }
}
