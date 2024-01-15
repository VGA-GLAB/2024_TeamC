using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class DamageView : MonoBehaviour
    {
        [SerializeField] float _destroyTime = 1;
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, _destroyTime);
        }
    }
}
