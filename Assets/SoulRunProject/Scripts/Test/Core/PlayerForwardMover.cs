using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤーを前に動かす処理
    /// </summary>
    public class PlayerForwardMover : MonoBehaviour, IPausable
    {
        [SerializeField] private bool _isPause = false;
        [SerializeField] private float _speed = 1.0f;
        public float Speed => _speed;
        
        private void FixedUpdate()
        {
            if (_isPause) return;
            transform.position += transform.forward * (_speed * Time.fixedDeltaTime);
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
    }
}
