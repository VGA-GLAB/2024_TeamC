using SoulRunProject.InGame;
using UnityEngine;

namespace SoulRunProject.Common
{
    public class EnemyBullet : BulletBase
    {
        [SerializeField, CustomLabel("弾速")] private float _speed = 5f;
        [SerializeField, CustomLabel("攻撃力")] private float _attackDamage = 5f;

        private bool _isHoming;
        private float _homingTime;
        private Vector3 _dir;
        private float _timer;
        private Transform _playerTransform;
        private void Awake()
        {
            base.Awake();
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnEnable()
        {
            _dir = Vector3.Normalize(_playerTransform.position - transform.position);
        }

        private void OnDisable()
        {
            _timer = 0;
        }

        public void InitializeHoming(bool homingFlag, float homingTime)
        {
            Debug.Log($"{homingFlag} : {homingTime}");
            _isHoming = homingFlag;
            _homingTime = homingTime;
        }

        public override void Move()
        {
            if (_isPause) return;
            _timer += Time.deltaTime;

            if (_isHoming)
            {
                Debug.Log("Hoge");
                if (_timer < _homingTime)
                {
                    _dir = Vector3.Normalize(_playerTransform.position - transform.position);
                }
                transform.position += _dir * (_speed * Time.fixedDeltaTime);
            }
            else
            {
                transform.position += Vector3.back * (_speed * Time.fixedDeltaTime);
            }
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerManager player))
            {
                player.Damage(_attackDamage);
                OnHit(other);
            }
        }

    }
}