using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SoulRunProject
{
    public class MapMover : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1.0f;

        private void FixedUpdate()
        {
            transform.position += Vector3.back * (Time.fixedDeltaTime * _moveSpeed);
        }
    }
}
