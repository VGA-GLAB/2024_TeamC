using UnityEngine;

namespace SoulRun.InGame
{
    /// <summary>
    /// 生成されると、生成時点でのプレイヤーの位置を参照してその方向に飛ぶ弾のクラス
    /// </summary>
    public class EnemyAttackShootingBullet : MonoBehaviour
    {
        [SerializeField] private Transform player; // プレイヤーのTransform
        private float speed = 5f; // 弾の移動速度
        private Vector3 _moveDir;

        private void Start()
        {
            //TODO FactoryMethodで参照を入れてあげるようにする
            player = FindAnyObjectByType<PlayerManager>()?.transform;

            if (player == null) return;
            Vector3 targetPosition = player.position;
            _moveDir = (targetPosition - transform.position).normalized;
        }

        private void Update()
        {
            MoveTowardsPlayer();
        }

        private void MoveTowardsPlayer()
        {
            if (player == null) return;

            // プレイヤーの位置に向かって移動
            transform.position += _moveDir * speed * Time.deltaTime;
        }
    }
}
