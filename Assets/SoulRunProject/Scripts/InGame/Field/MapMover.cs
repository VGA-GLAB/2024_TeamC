using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SoulRunProject.Framework;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject
{
    public class MapMover : MonoBehaviour, IPausable
    {
        [SerializeField] private float _moveSpeed = 1.0f;
        private bool _isPause = false;

        private void FixedUpdate()
        {
            if (_isPause) return;
            transform.position += Vector3.back * (Time.fixedDeltaTime * _moveSpeed);
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
    }
}
