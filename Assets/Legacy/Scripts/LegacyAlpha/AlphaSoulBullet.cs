using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class AlphaSoulBullet : MonoBehaviour
    {
        [SerializeField] float _speed = 1;
        [SerializeField] float _deathTime = 1;
        private float _currentTime = 0;

        // Update is called once per frame
        void Update()
        {
            _currentTime += Time.deltaTime;

            if ( _currentTime > _deathTime )
            {
                Destroy( this.gameObject);
            }

            transform.position += Vector3.forward * _speed;
        }
    }
}
