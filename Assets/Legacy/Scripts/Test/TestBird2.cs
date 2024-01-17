using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class TestBird2 : MonoBehaviour
    {
        public float amplitude = 1.0f; // Height of the sine wave
        public float frequency = 1.0f; // Speed of the sine wave

        private Vector3 startPosition;

        void Start()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            Vector3 tempPos = startPosition;
            tempPos.y += Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude;
            transform.position = tempPos;
        }
    }

}
