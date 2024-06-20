using UniRx;
using UnityEngine;
using UnityEditor;

namespace SoulRunProject.InGame
{
    /// <summary>
    /// ダメージUIを生成する
    /// </summary>
    [RequireComponent(typeof(DamageableEntity))]
    public class DisplayDamageUI : MonoBehaviour
    {
        [SerializeField] private DamageDisplay _damageUIPrefab;
        [SerializeField] private Vector3 _uiPosition;

        private CommonObjectPool _damageUIPool;
        /// <summary> 最後に表示したインスタンス </summary>
        private DamageDisplay _lastDisplayUI;

        private void Awake()
        {
            _damageUIPool = ObjectPoolManager.Instance.RequestPool(_damageUIPrefab);
            
            GetComponent<DamageableEntity>().OnDamaged += (damage, isCritical) =>
            {
                // 前回のUIが存在かつダメージ待機中であれば、ダメージの追加表示をする
                if (_lastDisplayUI && _lastDisplayUI.WaitingForNextDamage)
                {
                    _lastDisplayUI.AddDisplayDamage(damage);
                    return;
                }
                
                _lastDisplayUI = (DamageDisplay)_damageUIPool.Rent();
                DamageDisplay damageDisplay = _lastDisplayUI;
                damageDisplay.ResetDisplay(transform, _uiPosition, damage);
                damageDisplay.OnFinishedAsync.Take(1).Subscribe(_ => _damageUIPool.Return(damageDisplay));
            };
        }
        
        #if UNITY_EDITOR
        [CustomEditor(typeof(DisplayDamageUI))]
        public class FieldSegmentEditor : Editor
        {
            DisplayDamageUI _displayDamageUI;

            void OnEnable()
            {
                _displayDamageUI = target as DisplayDamageUI;
            }

            void OnSceneGUI()
            {
                //  ローカル座標をワールド座標に変換してくれる
                using (new Handles.DrawingScope(_displayDamageUI.transform.localToWorldMatrix))
                {
                    EditorGUI.BeginChangeCheck();
                    var newUIPos = Handles.PositionHandle(_displayDamageUI._uiPosition, Quaternion.identity);
                    GUIStyle style = new GUIStyle();
                    style.normal.textColor = Color.green;
                    Handles.Label(_displayDamageUI._uiPosition, "ダメージUI位置", style);
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(target, "Change Anchor Position");
                        _displayDamageUI._uiPosition = newUIPos;
                    }
                }
            }
        }
        #endif
    }
}
