using SoulRunProject.Common;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class BossFlameBallController : MonoBehaviour , IPausable
    {
        [SerializeField, CustomLabel("炎の床プレハブ")] private BossFlameFloorController _flameFloorPrefab;
        
        private float _flameFloorDamage;
        private Vector3 _lastPosition;
        private bool _isPause;

        private void Awake()
        {
            Destroy(gameObject, 10);
            Register();
        }

        private void OnDestroy()
        {
            UnRegister();
        }

        public void Initialize(float speed, float flameFloorDamage)
        {
            _flameFloorDamage = flameFloorDamage;
            _lastPosition = gameObject.transform.position;
            
            // move
            this.FixedUpdateAsObservable()
                .TakeUntilDestroy(this)
                .Subscribe(_ =>
                {
                    if (_isPause) return;
                    
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
            if (other.gameObject.CompareTag("Field"))
            {
                // 火の床生成
                BossFlameFloorController flameFloor = Instantiate(_flameFloorPrefab, other.gameObject.transform);
                flameFloor.transform.position = gameObject.transform.position;
                flameFloor.Initialize(_flameFloorDamage);
                CriAudioManager.Instance.PlaySE("SE_Fire_Hit"); // sound
                Destroy(gameObject);
            }
        }

        public void Register()
        {
            PauseManager.RegisterPausableObject(this);
        }

        public void UnRegister()
        {
            PauseManager.UnRegisterPausableObject(this);
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
        }
    }
}
