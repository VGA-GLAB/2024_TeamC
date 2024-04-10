using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SoulRunProject.Common
{
    [Serializable, Name("通常被ノックバック処理")]
    public class EntityKnockBackController
    {
        [SerializeField, Range(0, 100), Tooltip("ノックバック抵抗(%)")] float _resistanceValue;
        [SerializeField, Tooltip("ノックバック時間")] float _time;
        bool _isKnockBack;

        /// <summary>
        /// ノックバック処理
        /// </summary>
        public async void KnockBackAsync(float power, Vector3 direction, Rigidbody rb, CancellationToken ct)
        {
            // ノックバックが終わるまで次のノックバックは行わない
            if (_isKnockBack)
            {
                return;
            }

            _isKnockBack = true;
            power *= 10;
            var knockBackValue = power - power * _resistanceValue / 100;
            rb.AddForce(direction.normalized * knockBackValue, ForceMode.Impulse);
            try
            {
                // ノックバック時間を待機している
                await UniTask.Delay(TimeSpan.FromSeconds(_time), _isKnockBack = false,
                    cancellationToken: ct);
                // ノックバック時間の終了時に速度を0にする
                rb.velocity = Vector3.zero;
            }
            catch
            {
                // ignored
            }
        }
    }
}