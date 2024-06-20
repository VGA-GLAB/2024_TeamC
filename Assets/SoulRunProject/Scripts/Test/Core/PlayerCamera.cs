using System.Threading;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SoulRunProject
{
    /// <summary>
    /// プレイヤーのカメラクラス
    /// </summary>
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource _impulseSource;
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _firstPos;
        [SerializeField] private Transform _shakeObj;
        [Header("ダメージ時のカメラ設定")]
        [SerializeField] private float _shakeDur;
        [SerializeField] private float _shakePower;
        [Header("振動回数")]
        [SerializeField] private int _shakeVib;
        [Header("手ブレ値")]
        [SerializeField] private float _shakeRand;
        [Header("スナップするかどうか")]
        [SerializeField] private bool _isSnap;
        [Header("fadeするかどうか")]
        [SerializeField] private bool _isFade;
        private Vector3 _originalPos;
        private bool _shaking = false;

        private void Awake()
        {
            _offset = transform.position - _player.position;
            _originalPos = _shakeObj.position;
        }

        private void Update()
        {
            _shakeObj.position = new (_player.transform.position.x, transform.position.y, transform.position.z);
        }

        public async UniTask DoStartIngameMove(CancellationToken cts)
        {
            return;
        }

        public void DamageCam()
        {
            _impulseSource.GenerateImpulse(Vector3.one);
        }
        
        public void StartFollowPlayer()
        {
        }
    }
}
