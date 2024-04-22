using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using Cysharp.Threading.Tasks;
using SoulRunProject.InGame;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using DG.Tweening;

namespace SoulRunProject
{
    /// <summary>
    /// プレイヤーのカメラクラス
    /// </summary>
    public class PlayerCamera : MonoBehaviour
    {
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

        public async UniTask DoStartIngameMove(CancellationToken cts)
        {
            return;
            transform.position = _firstPos;
            await this.transform.DOMove(_player.position + _offset, 2f).WithCancellation(cts);
        }

        public void DamageCam()
        {
            if (_shaking) return;
            _shakeObj.transform.DOShakePosition(_shakeDur, _shakePower, _shakeVib, _shakeRand, _isSnap, _isFade)
                .OnStart(() => _shaking = true)
                .OnComplete(() =>
                {
                    _shakeObj.position = new (_player.transform.position.x, transform.position.y, transform.position.z);
                    _shaking = false;
                });
        }
        
        public void StartFollowPlayer()
        {
            // this.LateUpdateAsObservable().Subscribe(_ =>
            // {
            //     var pos = _player.position + _offset;
            //     transform.position = new Vector3(pos.x, transform.position.y, pos.z);
            // }).AddTo(_player.gameObject);
        }
    }
}
