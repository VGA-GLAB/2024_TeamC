using System;
using UnityEngine;

namespace SoulRunProject
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private SphereCollider _collider;
        private float _lifeTime;
        private float _attackDamage;
        private float _range;
        private float _speed;
        private int _penetration;
        

        public void Initialize(float lifeTime, float attackDamage, float range, float speed, int penetration)
        {
            _lifeTime = lifeTime;
            _attackDamage = attackDamage;
            _range = range;
            _speed = speed;
            _penetration = penetration;
            
            _collider.radius = range;
            Destroy(gameObject , lifeTime);
        }

        public virtual void Move()
        {
            this.transform.position += Vector3.forward * (_speed * Time.fixedDeltaTime);
        }
        
        private void FixedUpdate()
        {
            Move();
        }

        //当たり判定は弾側からとりたい
        private void OnCollisionEnter(Collision other)
        {
            
        }
        
    }
}
