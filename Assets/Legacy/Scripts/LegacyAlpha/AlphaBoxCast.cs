using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class AlphaBoxCast : MonoBehaviour
    {
        private BoxCollider boxCollider;
        Vector3 previousPosition;
        [SerializeField] GameObject _gem;
        [SerializeField] GameObject _damage;
        [SerializeField] CastType _castType;

        enum CastType
        {
            SoulBullet,
            Enemy,
            Gem,
        }

        void Start()
        {
            boxCollider = GetComponent<BoxCollider>();
            previousPosition = transform.position;
        }

        void Update()
        {
            if (previousPosition == null) return;
            Vector3 currentPosition = transform.position; // 現在の地点
            Vector3 direction = currentPosition - previousPosition; // 方向を計算

            float distance = direction.magnitude;
            direction.Normalize();

            RaycastHit hitInfo;
            if (Physics.BoxCast(previousPosition, boxCollider.size / 2, direction, out hitInfo, Quaternion.identity, distance))
            {
                if (_castType == CastType.SoulBullet)
                {
                    if (hitInfo.collider.tag == "Enemy")
                    {
                        Instantiate(_gem, hitInfo.collider.transform.position, Quaternion.identity);
                        Instantiate(_damage, hitInfo.collider.transform.position + Vector3.up * 2, Quaternion.identity);
                        AlphaExpAndScoreManager.Instance.AddScore(100);
                        Settings.Instance.seSource.PlayOneShot(Settings.Instance.deathSe);
                        Destroy(hitInfo.collider.gameObject);
                    }
                }
                else if (_castType == CastType.Gem)
                {
                    if (hitInfo.collider.GetComponent<MockPlayerController>())
                    {
                        Destroy(gameObject);
                    }
                }
            }
            previousPosition = currentPosition;
        }
    }
}
