using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    public class EnemyBulletController : BulletController
    {
        float _attackDamage;
        float _range;
        float _speed;

        public override void Move()
        {
            var trans = transform;
            trans.Translate(0, (trans.position.z + _speed) * Time.fixedDeltaTime, 0);
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerManager player))
            {
                OnHit(other);
                //player.Damage(_attackDamage);
            }
        }
    }
}