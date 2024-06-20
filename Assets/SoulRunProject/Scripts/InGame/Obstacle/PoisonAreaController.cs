using System;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject
{
    public class PoisonAreaController : MonoBehaviour
    {
        [SerializeField] private float _damageInterval;
        [SerializeField] private float _attackDamage;

        private PlayerManager _player;
        private bool _triggerFlag;
        private float _timer;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerManager>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (!_triggerFlag) return;

            if (_damageInterval < _timer)
            {
                _timer = 0;
                _player.Damage(_attackDamage);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _triggerFlag = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _triggerFlag = false;
            }
        }
    }
}