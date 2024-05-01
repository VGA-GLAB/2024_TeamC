using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    
    /// <summary>
    /// Enemyの弾丸生成クラス
    /// </summary>
    public class EnemyBulletShot : MonoBehaviour
    {
        [SerializeField, CustomLabel("生成する弾丸")] GameObject _bullet;

        /// <summary>
        /// エネミーの弾丸用オブジェクトプールから弾を確保する
        /// </summary>
        /// <param name="trans">生成座標</param>
        /// <returns>弾丸操作クラス</returns>
        public BulletController GenerateBullet(Transform trans)
        {
            var obj = EnemyObjectPool.Instance.GetPoolObject(_bullet, trans.position + Vector3.back);
            return obj.GetComponent<BulletController>();
        }
    }
}