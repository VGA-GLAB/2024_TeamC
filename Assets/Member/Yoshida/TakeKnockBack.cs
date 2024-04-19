using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("通常被ノックバック処理")]
    public class TakeKnockBack
    {
        [SerializeField, CustomLabel("ノックバック抵抗(%)"), Range(0, 100)] private float _resistanceValue;
        [SerializeField, CustomLabel("ノックバック完了にかかる時間")] private float _knockBackTime = 0.5f;
        bool _isKnockBack;
        private Sequence _sequence;
        /// <summary>
        /// ノックバック処理
        /// </summary>
        public async void KnockBack(Transform myTransform , float power, Vector3 direction)
        {
            // ノックバックが終わるまで次のノックバックは行わない
            if (_isKnockBack)　return;
            _isKnockBack = true;
            direction *= power * (100 - _resistanceValue);
            _sequence = DOTween.Sequence();
            _sequence.Append(myTransform.DOBlendableMoveBy(new Vector3(direction.x, 0f ,direction.z), _knockBackTime));
            _sequence.Insert(0f , myTransform.DOBlendableMoveBy(new Vector3(0f , direction.y ,0f), _knockBackTime / 2));
            _sequence.Insert(_knockBackTime / 2 , myTransform.DOBlendableMoveBy(new Vector3(0f , - direction.y ,0f), _knockBackTime / 2));
            await _sequence;
            _sequence = null;
            _isKnockBack = false;
        }

        public void Pause()
        {
            _sequence?.Pause();
        }

        public void Resume()
        {
            _sequence?.Restart();
        }
    }
}