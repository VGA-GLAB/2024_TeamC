using System.Collections;
using System.Collections.Generic;
using SoulRunProject.Framework;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤーを前に動かす処理
    /// </summary>
    public class PlayerForwardMover : MonoBehaviour, IInGameTime
    {
        [SerializeField] private bool _isPause = false;
        [SerializeField] private float _speed = 1.0f;
        
        private void FixedUpdate()
        {
            if (_isPause) return; 
            transform.position += transform.forward * (_speed);
        }

        public void SwitchPause(bool toPause)
        {
            _isPause = toPause;
        }
    }
}
