using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject
{
    public class PoisonAreaController : MonoBehaviour
    {
        [SerializeField] float _attackDamage;

        PlayerManager _player;
        bool _triggerFlag;
        
        // TODO 死亡時に処理を止める必要ある
        void FixedUpdate()
        {
            if (!_triggerFlag)
            {
                return;
            }
            
            _player.Damage(_attackDamage * Time.fixedDeltaTime);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out _player))
            {
                _triggerFlag = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out _player))
            {
                _triggerFlag = false;
            }
        }
    }
}