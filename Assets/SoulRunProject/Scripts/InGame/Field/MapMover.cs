using SoulRunProject.Common;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject
{
    public class MapMover : MonoBehaviour, IPausable
    {
        [SerializeField] private float _moveSpeed = 1.0f;
        private bool _isPause;

        private void Awake()
        {
            Register();
        }

        private void OnDestroy()
        {
            UnRegister();
        }

        private void FixedUpdate()
        {
            if (_isPause) return;
            transform.position += Vector3.back * (Time.fixedDeltaTime * _moveSpeed);
        }

        public void Register()
        {
            PauseManager.RegisterPausableObject(this);
        }

        public void UnRegister()
        {
            PauseManager.UnRegisterPausableObject(this);
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
    }
}
