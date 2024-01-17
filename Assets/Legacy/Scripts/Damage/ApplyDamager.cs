using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// ダメージ処理を行うクラス
    /// </summary>
    public class ApplyDamager : MonoBehaviour
    {
        [SerializeField] int _damage = 1;
        /// <summary> ダメージを与えられる相手の属性一覧 </summary>
        [SerializeField] ObjectDamageType _applyDamageType = new();
        ZDirectionBoxCast _boxCast;

        private void Start()
        {
            _boxCast = gameObject.AddComponent<ZDirectionBoxCast>();
        }

        private void Update()
        {
            CheckHitting();
        }

        /// <summary>
        /// 当たっているかを確認し、当たっているならダメージ処理を行う
        /// </summary>
        private void CheckHitting()
        {
            if (_boxCast.Hits == null) return;

            foreach (var hit in _boxCast.Hits)
            {
                if (hit.collider == null) continue;
                if (hit.collider.TryGetComponent<IDamage>(out var status))
                {
                    if (_applyDamageType.HasFlag(status.HitPoint.DamageType))
                    {
                        status.HitPoint.ApplyDamage(_damage);
                    }
                }
            }
        }
    }
}
