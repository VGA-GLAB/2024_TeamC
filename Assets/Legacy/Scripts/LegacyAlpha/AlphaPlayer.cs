using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulRun.InGame
{
    public class AlphaPlayer : MonoBehaviour
    {
        private BoxCollider boxCollider;
        Vector3 previousPosition;
        [SerializeField] private float _addExp = 50;

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
                if (hitInfo.collider.TryGetComponent(out AlphaGem gem))
                {
                    AlphaExpAndScoreManager.Instance.AddExp(_addExp);
                    Destroy(gem.gameObject);
                }
            }
            previousPosition = currentPosition;
        }
    }
}
