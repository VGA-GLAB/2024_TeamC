using System;
using System.Collections;
using System.Collections.Generic;
using SoulRunProject.InGameTest;
using UnityEngine;

namespace SoulRunProject
{
    public class TestCamera : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _offset;

        private void Awake()
        {
            _offset = transform.position - _player.position;
        }

        private void LateUpdate()
        {
            transform.position = _player.position + _offset;
        }
    }
}
