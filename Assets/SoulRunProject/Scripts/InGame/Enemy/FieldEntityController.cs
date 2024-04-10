using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 敵や障害物を管理するクラス
    /// </summary>
    public class FieldEntityController : MonoBehaviour, IInGameTime
    {
        [SerializeReference, SubclassSelector, Tooltip("敵の攻撃パターンを設定する")]
        protected IEntityAttacker _attacker;

        [SerializeReference, SubclassSelector, Tooltip("敵の移動パターンを設定する")]
        protected IEntityMover _mover;

        [SerializeField, Tooltip("敵のパラメータを設定する")]
        protected Status _status;

        [SerializeField, Tooltip("ドロップデータ")] LootTable _lootTable;
        [SerializeField] protected PlayerManager _playerManager;
        [SerializeField] HitDamageEffectManager _hitDamageEffectManager;

        [SerializeReference, SubclassSelector, Tooltip("ノックバック処理")]
        EntityKnockBackController _useKnockBack;

        CancellationToken _ct;
        Rigidbody _rb;

        /// <summary>このエンティティが敵かどうかを判別するbool</summary>
        bool _isEnemy;

        public Status Status => _status;

        void Start()
        {
            InitializeEntityStatus();
            SetActive();
            // Rigidbodyの有無によって、敵か障害物を判定する
            // TODO ノックバック処理がRigidbody式から変更時にはこの判定も変えよう
            _isEnemy = _useKnockBack != null && TryGetComponent(out _rb);
            _ct = this.GetCancellationTokenOnDestroy();
        }

        void Update()
        {
            _mover?.OnUpdateMove(this.transform, _playerManager.transform);
        }

        /// <summary>
        /// 各行動の初期化処理を行うメソッド
        /// </summary>
        void InitializeEntityStatus()
        {
            _attacker?.GetAttackStatus(_status);
            _mover?.GetMoveStatus(_status);
            _status = _status.Copy();
        }

        void SetActive()
        {
            _attacker?.OnStart();
            _mover?.OnStart();
        }

        public void SetPlayer(PlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void Damage(float damage)
        {
            _status.Hp -= damage;
            if (_status.Hp <= 0)
            {
                Death();
            }

            if (_hitDamageEffectManager)
            {
                _hitDamageEffectManager.HitFadeBlinkWhite();
            }
        }

        void Death()
        {
            if (_lootTable)
            {
                ItemDropManager.Instance.Drop(_lootTable, transform.position, _playerManager.CurrentStatus);
            }

            _attacker?.Stop();
            _mover?.Stop();
            Destroy(gameObject);
        }

        public void SwitchPause(bool toPause)
        {
            if (toPause)
            {
                _attacker?.Stop();
                _mover?.Stop();
            }
            else
            {
                _attacker?.OnStart();
                _mover?.OnStart();
            }
        }

        /// <summary>
        /// 接触時ノックバック処理を行う
        /// </summary>
        void OnTriggerEnter(Collider other)
        {
            if (!_isEnemy || !other.TryGetComponent(out SkillKnockBackController skill))
            {
                return;
            }

            var power = skill.Power;
            var direction = other.GetComponent<Transform>().position;
            _useKnockBack.KnockBackAsync(power, direction, _rb, _ct);
        }
    }
}