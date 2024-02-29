using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRunProject.InGameTest
{
    public class TestPlayerForwardMover : MonoBehaviour
    {
        [SerializeField] private bool _isActivated = false;
        [SerializeField] private float _speed = 1.0f;
        
        public void Activate()
        {
            gameObject.SetActive(true);
        }
        
        private void Update()
        {
            if (_isActivated)
                transform.position += transform.forward * (_speed * Time.deltaTime);
        }
    }
}
