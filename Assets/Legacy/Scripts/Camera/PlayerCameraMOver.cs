using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using VContainer.Unity;
using VContainer;

namespace SoulRun.InGame
{
    /// <summary>
    /// プレイヤーのカメラを動かすクラス
    /// </summary>
    public class PlayerCameraMover : ILateTickable
    {
        private PlayerManager _player;
        private Vector3 _firstCameraPos;
        private Vector3 _cameraOffset = new (0, 1.5f, -5f);
        private Vector3 _firstCameraOffset = new (0, 20, 0);
        private float _firstCamMoveDur = 3;
        private bool _firstCamMoveDone = false;

        [Inject]
        public PlayerCameraMover(PlayerManager player, InGameManager inGameManager)
        {
            _firstCameraPos = player.transform.position + _firstCameraOffset;
            Debug.Log(_firstCameraPos);
            _player = player;
            inGameManager.OnGameStart += () => OnStartGameCameraMove();
        }

        /// <summary>
        /// ステージ開始時のカメラの移動を行う
        /// </summary>
        /// <returns></returns>
        public async UniTask OnStartGameCameraMove()
        {
            Camera.main.transform.position = _firstCameraPos;
            await Camera.main.transform.DOMove(_player.transform.position + _cameraOffset, _firstCamMoveDur);
            _firstCamMoveDone = true;
        }

        public void LateTick()
        {
            if (_firstCamMoveDone)
                Camera.main.transform.position = _player.transform.position + _cameraOffset;
        }
    }
}
