using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using SoulRunProject.InGame;
using UniRx;
using UnityEngine;

namespace SoulRunProject.Common
{
    /// <summary>
    /// 範囲攻撃クラス
    /// </summary>
    public class AoEController : MonoBehaviour
    {
        [SerializeField] private float _rotateTime = 2f;
        HashSet<DamageableEntity> _entities = new();
        private AoESkillParameter _param;
        private PlayerManager _playerManager;

        private void OnEnable()
        {
            transform.DORotate(new Vector3(0, 360, 0), _rotateTime, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart)
                .SetLink(gameObject)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        public void ApplyParameter(in AoESkillParameter param, PlayerManager playerManager)
        {
            _param = param;
            _playerManager = playerManager;
            param.ObserveEveryValueChanged(x => x.Size).Subscribe(x => transform.localScale = Vector3.one * x).AddTo(this);
        }

        void FixedUpdate()
        {
            _entities = _entities.Where(entity => entity && entity.gameObject.activeSelf).ToHashSet();
            // OnTriggerExitする前にDestroyすることがあるので、
            // Whereでnullチェックとアクティブかどうかをチェックしてからダメージ処理
            foreach (var entity in _entities)
            {
                entity.Damage(_param.BaseAttackDamage * Time.fixedDeltaTime, useSE: false);

                if (entity.IsEnemy) // 敵に対するヒット数によってもらえるソウルが増える
                {
                    _playerManager.AddSoul(_param.GetSoulPerSec * Time.fixedDeltaTime);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DamageableEntity entity))
            {
                _entities.Add(entity);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out DamageableEntity entity))
            {
                _entities.Remove(entity);
            }
        }
    }
}
