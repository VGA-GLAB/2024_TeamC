using SoulRunProject.Framework;
using UnityEditor;
using UnityEngine;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// プレイヤー移動
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IInGameTime
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _grav;
        [SerializeField] private float _yAxisGroundLine = 0;
        [SerializeField, HideInInspector] private float _moveRangeMin;
        [SerializeField, HideInInspector] private float _moveRangeMax;

        private Rigidbody _rb;
        private bool _isGround;
        private Vector3 _playerVelocity;
        private bool _inPause;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            _yAxisGroundLine = transform.position.y;
        }

        private void Update()
        {
            if (_inPause) return;
            LimitPosition();
            GroundCheck();
            _rb.velocity = _playerVelocity;
        }

        private void FixedUpdate()
        {
            if (_inPause) return;
            if (_isGround && _playerVelocity.y <= 0)
            {
               
                _playerVelocity.y = 0;
            }
            else
            {
                _playerVelocity.y -= _grav * Time.fixedDeltaTime;
            }
        }

        public void InputHorizontal(float horizontal)
        {
            if (_inPause) return;
            _playerVelocity.x = horizontal * _moveSpeed;
        }

        public void Jump()
        {
            if (_inPause) return;
            
            if (_isGround)
            {
                _playerVelocity.y = _jumpPower;
            }
        }

        private void GroundCheck()
        {
            if (transform.position.y <= _yAxisGroundLine)
            {
                Vector3 pos = transform.position;
                pos.y = _yAxisGroundLine;
                transform.position = pos;
                _isGround = true;
            }
            else
            {
                _isGround = false;
            }
        }

        public void SwitchPause(bool toPause)
        {
            _inPause = toPause;
            
            if (toPause)
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
            // x マイナス側の制限
            if (transform.position.x <= _moveRangeMin)
            {
                // 位置の制限
                Vector3 pos = transform.position;
                pos.x = _moveRangeMin;
                transform.position = pos;
                // Velocityの制限
                _playerVelocity.x = Mathf.Clamp(_playerVelocity.x, 0, _moveSpeed);
                return;
            }

            // x プラス側の制限
            if (transform.position.x >= _moveRangeMax)
            {
                // 位置の制限
                Vector3 pos = transform.position;
                pos.x = _moveRangeMax;
                transform.position = pos;
                // Velocityの制限
                _playerVelocity.x = Mathf.Clamp(_playerVelocity.x, -_moveSpeed, 0);
            }
        }
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            SceneView.RepaintAll();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Vector3 posX = Vector3.right * _moveRangeMin;
            Vector3 posY = Vector3.right * _moveRangeMax;
            Gizmos.DrawLine(posX, posY);
            Gizmos.DrawLine(posX + Vector3.up, posX - Vector3.up);
            Gizmos.DrawLine(posY + Vector3.up, posY - Vector3.up);
        }
        
        /// <summary>
        /// move rangeの拡張
        /// </summary>
        [CustomEditor(typeof(PlayerMovement))]
        public class PlayerMovementEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                PlayerMovement playerMovement = target as PlayerMovement;

                DrawDefaultInspector();
                
                EditorGUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 32;
                GUILayoutOption[] fieldOptions = new GUILayoutOption[]
                {
                    GUILayout.MinWidth(0),
                    GUILayout.MaxWidth(98)
                };
                EditorGUILayout.LabelField("X座標の移動範囲", fieldOptions);
                
                GUILayout.FlexibleSpace();
                
                fieldOptions = new GUILayoutOption[]
                {
                    GUILayout.MinWidth(84),
                    GUILayout.MaxWidth(84 < EditorGUIUtility.currentViewWidth * 0.27f? EditorGUIUtility.currentViewWidth * 0.27f : 84)
                };
                EditorGUI.BeginChangeCheck();
                playerMovement._moveRangeMin =
                    EditorGUILayout.FloatField("Min", playerMovement._moveRangeMin, fieldOptions);
                GUILayout.Space(EditorGUIUtility.currentViewWidth * 0.03f);
                playerMovement._moveRangeMax =
                    EditorGUILayout.FloatField("Max", playerMovement._moveRangeMax, fieldOptions);
                EditorGUILayout.EndHorizontal();

                if (playerMovement._moveRangeMin > playerMovement._moveRangeMax)
                {
                    playerMovement._moveRangeMin = playerMovement._moveRangeMax;
                }
                
                if (EditorGUI.EndChangeCheck())
                {
                    SceneView.RepaintAll();
                }
                
                Undo.RecordObject(playerMovement, "set playerMovement");
            }
        }
        #endif
    }
}
