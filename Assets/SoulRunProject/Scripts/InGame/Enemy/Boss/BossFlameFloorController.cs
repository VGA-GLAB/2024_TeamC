using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject
{
    public class BossFlameFloorController : MonoBehaviour
    {
        private float _damage;
        
        public void Initialize(float damage)
        {
            _damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerManager player))
            {
                player.Damage(_damage);
            }
        }
    }
}
