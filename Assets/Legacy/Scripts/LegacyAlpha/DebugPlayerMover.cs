using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class DebugPlayerMover : MonoBehaviour
    {
        PlayerMover _playerMover;
        // Start is called before the first frame update
        void Start()
        {
            _playerMover = GetComponent<PlayerMover>();
        }

        // Update is called once per frame
        void Update()
        {
            _playerMover.ManualUpdate(Time.deltaTime);
            _playerMover.MoveForward(Time.deltaTime);
        }
    }
}
