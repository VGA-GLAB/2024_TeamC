using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class TestObjectFactory : MonoBehaviour
    {
        [SerializeField] UnityEngine.GameObject _obstacle;
        [SerializeField] float _genNum = 1000;
        [SerializeField] Vector3 _first = new Vector3(-7, 1.625f, 5);
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < _genNum; i++)
            {
                Instantiate(_obstacle, new Vector3(_first.x, _first.y, _first.z * i), Quaternion.identity);
            }
        }
    }
}
