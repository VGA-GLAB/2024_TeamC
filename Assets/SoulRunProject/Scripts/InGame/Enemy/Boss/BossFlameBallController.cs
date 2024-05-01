using SoulRunProject.Common;
using UniRx;
using UniRx.Triggers;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class BossFlameBallController : MonoBehaviour
    {
        [SerializeField, CustomLabel("炎の床プレハブ")] private BossFlameFloorController _flameFloorPrefab;
        
        private float _flameFloorDamage;
        private Vector3 _lastPosition;
        
        public void Initialize(float speed, float flameFloorDamage)
        {
            _flameFloorDamage = flameFloorDamage;
            _lastPosition = gameObject.transform.position;
            
            // move
            this.FixedUpdateAsObservable()
                .TakeUntilDestroy(this)
                .Subscribe(_ =>
                {
                    transform.position += transform.forward * speed * Time.fixedDeltaTime;
                    
                    // 当たり判定
                    Vector3 currentPos = gameObject.transform.position;
                    RaycastHit[] hits = Physics.RaycastAll(_lastPosition, currentPos - _lastPosition,
                        Vector3.Distance(_lastPosition, currentPos));

                    foreach (var hit in hits)
                    {
                        if (hit.transform.TryGetComponent(out FieldSegment field))
                        {
                            OnHitFloor(hit.point, field.gameObject);
                            break;
                        }
                    }
                });
        }

        void OnHitFloor(Vector3 hitPos, GameObject floorObj)
        {
            BossFlameFloorController flameFloor = Instantiate(_flameFloorPrefab, floorObj.transform);
            flameFloor.transform.position = hitPos;
            flameFloor.Initialize(_flameFloorDamage);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out FieldSegment field))
            {
                BossFlameFloorController flameFloor = Instantiate(_flameFloorPrefab, other.gameObject.transform);
                flameFloor.transform.position = gameObject.transform.position; //
                flameFloor.Initialize(_flameFloorDamage);
                Destroy(gameObject);
            }
        }
    }
}
