using System;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤーのインプットを管理する
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        public event Action<float> HorizontalAction; 
        public event Action JumpAction;

        private void Update()
        {
            HorizontalAction?.Invoke(Input.GetAxisRaw("Horizontal"));
            
            if (Input.GetButtonDown("Jump"))
            {
                JumpAction?.Invoke();
            }
        }
    }
}
