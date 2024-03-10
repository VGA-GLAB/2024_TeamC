using UnityEngine;
using SoulRunProject.Common;
using SoulRunProject.SoulMixScene;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// 敵や障害物を管理するクラス
    /// </summary>
    public class FieldEntityController : MonoBehaviour
    {
        [SerializeReference, SubclassSelector, Tooltip("敵の攻撃パターンを設定する")] protected IEntityAttacker _attacker;
        [SerializeReference, SubclassSelector, Tooltip("敵の移動パターンを設定する")] protected IEntityMover _mover;
        [SerializeField, Tooltip("敵のパラメータを設定する")] protected Status _status;
        [SerializeField] protected PlayerManager _playerManager;
        void Start()
        {
            InitializeEntityStatus();
            Active();
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
        }
        
        public void Active()
        {
            _attacker?.OnStart();
            _mover?.OnStart();
        }
    }
}