using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SoulRunProject.Audio;
using SoulRunProject.Common;
using UniRx;
using UniRx.Triggers;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤー移動
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IPlayerPausable
    {
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _grav;
        [SerializeField, CustomLabel("地面の高さ")] private float _yAxisGroundLine;

        [SerializeField, CustomLabel("Pivotと接地点との距離")]
        private float _distanceBetweenPivotAndGroundPoint;

        [SerializeField, HideInInspector] private float _xMoveRangeMin;
        [SerializeField, HideInInspector] private float _xMoveRangeMax;

        private Rigidbody _rb;
        private readonly BoolReactiveProperty _isGround = new BoolReactiveProperty(false);
        private Vector3 _playerVelocity;
        private bool _inPause;
        private Guid _spin;

        public BoolReactiveProperty IsGround => _isGround;
        public event Action OnJumped;

        /// <summary> プレイヤー地点の地面の高さ </summary>
        public float GroundHeight => _yAxisGroundLine;

        public float DistanceBetweenPivotAndGroundPoint => _distanceBetweenPivotAndGroundPoint;

        private async void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;

            _isGround.AddTo(this);
            this.OnDestroyAsObservable().Subscribe(_ => OnJumped = null);
            
            await UniTask.DelayFrame(3);// ステージが生成されるまで待機
            _isGround.SkipLatestValueOnSubscribe().Subscribe(flag =>
            {
                if (flag)
                    CriAudioManager.Instance.Stop(CriAudioType.CueSheet_SE, _spin);
                else
                    _spin = CriAudioManager.Instance.Play(CriAudioType.CueSheet_SE, "SE_Spin");
            }).AddTo(this);
        }

        private void Start()
        {
            PauseManager.IsPause.Subscribe(isPause =>
            {
                if (isPause) CriAudioManager.Instance.Pause(CriAudioType.CueSheet_SE, _spin);
                else CriAudioManager.Instance.Resume(CriAudioType.CueSheet_SE, _spin);
            });

            SceneManager.sceneLoaded += (arg0, mode) =>
            {
                CriAudioManager.Instance.Stop(CriAudioType.CueSheet_SE, _spin);
            };
        }

        private void Update()
        {
            if (_inPause) return;
            LimitPosition();
            GroundCheck();
        }

        private void FixedUpdate()
        {
            if (_inPause) return;

            if (_isGround.Value && _playerVelocity.y < 0)
            {
                _playerVelocity.y = 0;
            }
            else
            {
                _playerVelocity.y -= _grav * Time.fixedDeltaTime;
            }

            _rb.velocity = _playerVelocity;
        }

        public void InputMove(Vector2 moveInput)
        {
            _playerVelocity.x = moveInput.x * _moveSpeed;
        }

        public void Jump()
        {
            if (_inPause) return;

            if (_isGround.Value)
            {
                _playerVelocity.y = _jumpPower;
                _isGround.Value = false;
                CriAudioManager.Instance.Play(CriAudioType.CueSheet_SE, "SE_Jump");
                OnJumped?.Invoke();
            }
        }

        /// <summary>
        /// 地面を検出してpositionと音を調整する
        /// </summary>
        private void GroundCheck()
        {
            // 地面の高さ判定
            RaycastHit[] hits = Physics.RaycastAll(transform.position + Vector3.up * 10, Vector3.down, 20);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag("Field"))
                {
                    _yAxisGroundLine = hit.point.y;
                    break;
                }
            }

            if (transform.position.y <= _yAxisGroundLine + DistanceBetweenPivotAndGroundPoint)
            {
                Vector3 pos = transform.position;
                pos.y = _yAxisGroundLine + DistanceBetweenPivotAndGroundPoint;
                transform.position = pos;

                if (!_isGround.Value)
                {
                    CriAudioManager.Instance.Play(CriAudioType.CueSheet_SE, "SE_Landing");
                    _isGround.Value = true;
                }
            }
            else if (_isGround.Value)
            {
                _isGround.Value = false;
            }
        }

        public void Pause(bool isPause)
        {
            _inPause = isPause;

            if (isPause)
            {
                _rb.Sleep();
            }
            else
            {
                _rb.WakeUp();
            }
        }

        /// <summary>
        /// プレイヤーのポジションを一定範囲内に限定する
        /// </summary>
        void LimitPosition()
        {
            // x座標軸の制限
            if (transform.position.x <= _xMoveRangeMin) // x マイナス側の制限
            {
                // 位置の制限
                Vector3 pos = transform.position;
                pos.x = _xMoveRangeMin;
                transform.position = pos;
                // Velocityの制限
                _playerVelocity.x = Mathf.Clamp(_playerVelocity.x, 0, _moveSpeed);
            }
            else if (transform.position.x >= _xMoveRangeMax) // x プラス側の制限
            {
                // 位置の制限
                Vector3 pos = transform.position;
                pos.x = _xMoveRangeMax;
                transform.position = pos;
                // Velocityの制限
                _playerVelocity.x = Mathf.Clamp(_playerVelocity.x, -_moveSpeed, 0);
            }
        }

        public void RotatePlayer(Vector2 input)
        {
            _playerAnimator.SetFloat("Direction", input.x);
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            SceneView.RepaintAll();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Vector3 leftPos = Vector3.right * _xMoveRangeMin;
            Vector3 rightPos = Vector3.right * _xMoveRangeMax;
            Gizmos.DrawLine(leftPos, rightPos);
            Gizmos.DrawLine(leftPos + Vector3.up, leftPos - Vector3.up);
            Gizmos.DrawLine(rightPos + Vector3.up, rightPos - Vector3.up);
        }

        /// <summary>
        /// move rangeの拡張
        /// </summary>
        [CustomEditor(typeof(PlayerMovement))]
        public class PlayerMovementEditor : Editor
        {
            private PlayerMovement _playerMovement;

            private void Awake()
            {
                _playerMovement = target as PlayerMovement;
            }

            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                EditorGUILayout.BeginHorizontal();
                float width = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 32;
                GUILayoutOption[] fieldOptions = new GUILayoutOption[]
                {
                    GUILayout.MinWidth(0),
                    GUILayout.MaxWidth(98)
                };
                EditorGUILayout.LabelField("X軸座標の移動範囲", fieldOptions);

                GUILayout.FlexibleSpace();

                fieldOptions = new GUILayoutOption[]
                {
                    GUILayout.MinWidth(84),
                    GUILayout.MaxWidth(84 < EditorGUIUtility.currentViewWidth * 0.27f
                        ? EditorGUIUtility.currentViewWidth * 0.27f
                        : 84)
                };

                EditorGUI.BeginChangeCheck();
                _playerMovement._xMoveRangeMin =
                    EditorGUILayout.FloatField("Min", _playerMovement._xMoveRangeMin, fieldOptions);
                GUILayout.Space(EditorGUIUtility.currentViewWidth * 0.03f);
                _playerMovement._xMoveRangeMax =
                    EditorGUILayout.FloatField("Max", _playerMovement._xMoveRangeMax, fieldOptions);
                EditorGUILayout.EndHorizontal();

                if (_playerMovement._xMoveRangeMin > _playerMovement._xMoveRangeMax)
                {
                    _playerMovement._xMoveRangeMin = _playerMovement._xMoveRangeMax;
                }

                if (EditorGUI.EndChangeCheck())
                {
                    SceneView.RepaintAll();
                }

                Undo.RecordObject(_playerMovement, "set playerMovement");
            }
        }
#endif
    }
}