using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class TestPlayerForwardMover : MonoBehaviour
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
