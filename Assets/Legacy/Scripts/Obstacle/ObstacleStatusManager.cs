using UnityEngine;
using UniRx;
using static SoulRun.InGame.HitPoint;

namespace SoulRun.InGame
{
    /// <summary>
    /// 障害物が持つ。体力が0になると破壊され、スコアが加算される
    /// </summary>
    public class ObstacleStatusManager : MonoBehaviour
    {
        [SerializeField] HitPoint _hitPoint = new HitPoint(ObjectDamageType.Obstacle, 1);
        //private float _addScore = 100;


        private void Start()
        {
            _hitPoint.HPChanged.Where(hp => hp <= 0).Subscribe(_ => Death());
        }

        /// <summary>
        /// Playerの死亡処理
        /// </summary>
        private void Death()
        {
            //TODO Factoryで処理をする
            Destroy(this.gameObject);
        }
    }
}
