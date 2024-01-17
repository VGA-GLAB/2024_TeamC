using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace SoulRun.InGame
{
    public class AlphaMissile : MonoBehaviour
    {
        Vector3 _velocity;
        [SerializeField] NumRange _vert;
        [SerializeField] NumRange _vert2;
        [SerializeField] NumRange _hori;
        [SerializeField] float _pow;
        [SerializeField] float _speed = 1;
        Vector3 _pos;
        [SerializeField] public Transform _target;
        [SerializeField] Vector3 _cashPos;
        [SerializeField] public float _period;
        [SerializeField] GameObject _gem;

        private void Start()
        {
            _pos = transform.position;
            var h = Random.Range(_hori.Min, _hori.Max);

            if (Random.Range(0, 2) == 1)
            {
                var v = Random.Range(_vert.Min, _vert.Max);
                _velocity = new Vector3(h, v, 1 * _speed) * _pow;
            }
            else
            {
                var v = Random.Range(_vert2.Min, _vert2.Max);
                _velocity = new Vector3(h, v, 1 * _speed) * _pow;
            }
            
        }

        private void Update()
        {
            var accel = Vector3.zero;

            if (!_target)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                var diff = _target.position - _pos;
                accel += (diff - _velocity * _period) * 2f / (_period * _period);
            }

            _period -= Time.deltaTime;
            if (_period < 0 )
            {
                Instantiate(_gem, _target.transform.position, Quaternion.identity);
                Settings.Instance.PlaySE();
                if (AlphaExpAndScoreManager.Instance) AlphaExpAndScoreManager.Instance.AddScore(100);

                Destroy(_target.gameObject);
                Destroy(this.gameObject);
            }

            _velocity += accel * Time.deltaTime;
            _pos += _velocity * Time.deltaTime;
            transform.position = _pos;
            _cashPos = _target.position;
        }
    }
}
