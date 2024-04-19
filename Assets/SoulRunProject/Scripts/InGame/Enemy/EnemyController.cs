using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;
using UnityEngine;

namespace SoulRunProject.InGame
{
    [RequireComponent(typeof(DamageableEntity))]
    public class EnemyController: MonoBehaviour, IPausable
    {
        [SerializeReference, SubclassSelector, Tooltip("敵の攻撃パターンを設定する")]
        protected EntityAttacker _attacker;
        [SerializeReference, SubclassSelector, Tooltip("敵の移動パターンを設定する")]
        protected EntityMover _mover;
        protected Transform _playerTransform;
        

        /// <summary>
        /// 各行動の初期化処理を行うメソッド
        /// </summary>
        void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            _attacker?.OnStart();
            _mover?.OnStart();
        }
        void Update()
        {
            _mover?.OnUpdateMove(this.transform, _playerTransform);
            _attacker?.OnUpdateAttack(this.transform, _playerTransform);
        }
        public void Pause(bool isPause)
        {
            if (isPause)
            {
                _attacker?.Pause();
                _mover?.Pause();
            }
            else
            {
                _attacker?.Resume();
                _mover?.Resume();
            }
        }
    }
}