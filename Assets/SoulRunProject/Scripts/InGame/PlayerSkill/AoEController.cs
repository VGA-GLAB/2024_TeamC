using System;
using System.Collections.Generic;
using System.Linq;
using SoulRunProject.InGame;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// 範囲攻撃クラス
    /// </summary>
    public class AoEController : MonoBehaviour
    {
        HashSet<FieldEntityController> _entities = new();
        float _attackDamage;

        public void Initialize(float attackDamage, float range)
        {
            _attackDamage = attackDamage;
            transform.localScale = new Vector3(range, range, range);
        }

        void FixedUpdate()
        {
            // OnTriggerExitする前にDestroyすることがあるので、
            // Whereでnullチェックしてからダメージ処理
            foreach (var entity in _entities.Where(entity => entity))
            {
                entity.Damage(_attackDamage * Time.fixedDeltaTime);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out FieldEntityController entity))
            {
                _entities.Add(entity);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out FieldEntityController entity))
            {
                _entities.Remove(entity);
            }
        }
    }
}
