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
        [SerializeReference, SubclassSelector, Tooltip("敵の攻撃パターンを設定する")] IEntityAttacker _attack;
        [SerializeReference, SubclassSelector, Tooltip("敵の移動パターンを設定する")] IEntityMover _move;
        [SerializeField, Tooltip("敵のパラメータを設定する")] Status _enemyData;

        void Start()
        {
            InitializeEntityStatus();
        }

        /// <summary>
        /// 各行動の初期化処理を行うメソッド
        /// </summary>
        void InitializeEntityStatus()
        {
            _attack?.GetAttackStatus(_enemyData);
            _move?.GetMoveStatus(_enemyData);
        }
        
        public void Active()
        {
            _attack?.Attack();
            _move?.Move(null, null);//    TODO: 一時的にnull入れた
        }
    }
}