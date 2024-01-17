using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{

    public class AlphaGem : MonoBehaviour
    {
        [SerializeField] float _speed = 10;

        // Update is called once per frame
        private void FixedUpdate()
        {
            transform.position += Vector3.forward * -_speed;

            if (transform.position.z < -10 )
            {
                Destroy(gameObject);
            }

            if (transform.position.y < 0.26 )
            {
                transform.position = new Vector3(transform.position.x, 0.26f, transform.position.z);
            }
        }
    }
}
