using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class AlphaTreeObstale : MonoBehaviour
    {
        [SerializeField] float _activeDis = 100;
        private PlayerMover _playerMover;
        bool _active = false;
        [SerializeField] bool _isRight = false;

        // Start is called before the first frame update
        void Start()
        {
            _playerMover = FindAnyObjectByType<PlayerMover>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(_playerMover.transform.position, transform.position) <= _activeDis && _active == false)
            {
                if (!_isRight)
                    transform.DORotate(new Vector3(0, 0, -90), 2);
                else
                    transform.DORotate(new Vector3(0, 0, 90), 2);
                _active = true;
            }
        }
    }
}
