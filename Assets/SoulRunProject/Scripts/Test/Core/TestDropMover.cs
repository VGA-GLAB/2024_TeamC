using SoulRunProject.Common;
using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject
{
    /// <summary>
    /// 落下物をテストで動かすクラス
    /// </summary>
    public class TestDropMover : MonoBehaviour, IPausable
    {
        private float _speed = 20.0f;
        private bool _isPause = false;

        private void Awake()
        {
            Register();
        }

        private void OnDestroy()
        {
            UnRegister();
        }

        // Update is called once per frame
        void Update()
        {
            if (_isPause) return;
            transform.position += _speed * Time.deltaTime * -Vector3.forward;
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
