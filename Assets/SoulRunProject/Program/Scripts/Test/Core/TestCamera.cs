using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.InGame;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using DG.Tweening;

namespace SoulRunProject
{
    public class TestCamera : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _firstPos;

        private void Awake()
        {
            _offset = transform.position - _player.position;
        }

        public async UniTask DoStartIngameMove(CancellationToken cts)
        {
            transform.position = _firstPos;
            await this.transform.DOMove(_player.position + _offset, 2f).WithCancellation(cts);
        }
        
        public void StartFollowPlayer()
        {
            this.LateUpdateAsObservable().Subscribe(_ =>
            {
                var pos = _player.position + _offset;
                transform.position = new Vector3(pos.x, transform.position.y, pos.z);
            });
        }
    }
}
