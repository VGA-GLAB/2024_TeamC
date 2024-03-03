using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤーを前に動かす処理
    /// </summary>
    public class PlayerForwardMover : MonoBehaviour
    {
        [SerializeField] private bool _isActivated = false;
        [SerializeField] private float _speed = 1.0f;
        
        public void IsActivate(bool isActivate)
        {
            _isActivated = isActivate;
        }
        
        private void FixedUpdate()
        {
            if (_isActivated)
                transform.position += transform.forward * (_speed);
        }
    }
}
