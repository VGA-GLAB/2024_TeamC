using System;
using SoulRunProject.InGame;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoulRunProject.Common
{
    public enum PlayerSkill
    {
        SoulBullet = 0,
        HolyField = 1,
        SoulSword = 2,
        SoulShell = 3,
        SoulRay = 4,
        SoulOfHealing = 5,
        SoulFrame = 6
    }

    /// <summary>
    ///     スキルの基底クラス
    /// </summary>
    [Serializable]
    [Name("スキルの基底クラス")]
    public abstract class SkillBase : ScriptableObject, IPausable
    {
        [SerializeField] PlayerSkill _skillType;
        [SerializeField] private string _skillName;
        [SerializeField, Tooltip("スキルの説明文")] private string _explanatoryText;
        [SerializeField] private Sprite _skillIcon;
        [SerializeField] [Header("スキルの最大レベル")] public int MaxSkillLevel = 5;

        [SerializeField] [Header("レベルアップイベントデータ")]
        protected SkillLevelUpEvent SkillLevelUpEvent;

        [SerializeReference]
        protected ISkillParameter _skillParam;

        protected bool _isPause;

        int _currentLevel = 1;

        public PlayerSkill SkillType => _skillType;
        public string SkillName => _skillName;
        public string ExplanatoryText => _explanatoryText;
        public Sprite SkillIcon => _skillIcon;
        public int CurrentLevel => _currentLevel;

        private int _elementCount;
        private void OnValidate()
        {
            int currentElementCount = SkillLevelUpEvent.LevelUpType.LevelUpEventListList.Count;
            if (_elementCount != currentElementCount)
            {
                if (_elementCount < currentElementCount)
                {
                    Debug.Log("RefreshElement");
                    SkillLevelUpEvent.LevelUpType.RefreshElement();
                }
                _elementCount = currentElementCount;
            }
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            _elementCount = SkillLevelUpEvent.LevelUpType.LevelUpEventListList.Count;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            InitializeParamOnSceneLoaded();
        }

        /// <summary>
        ///     シーンロード時にパラメータを初期化するように登録する。
        /// </summary>
        public virtual void InitializeParamOnSceneLoaded()
        {
            _skillParam.InitializeParamOnSceneLoaded();
            _currentLevel = 1;
            _isPause = false;
        }

        /// <summary> スキルレベルアップ可能かどうか </summary>
        public bool CanLevelUp()
        {
            return _currentLevel <= MaxSkillLevel;
        }

        public virtual void StartSkill()
        {
        }

        public virtual void UpdateSkill(float deltaTime)
        {
        }

        /// <summary>レベルアップ時イベント</summary>
        public virtual void OnLevelUp()
        {
        }
        
        /// <summary>スキル進化</summary>
        public void LevelUp()
        {
            _currentLevel++;
            if (CanLevelUp())
            {
                SkillLevelUpEvent.LevelUp(_currentLevel, _skillParam);
                OnLevelUp();
            }
            else
            {
                Debug.LogError("レベル上限を超えています。");
            }
        }

        public void Pause(bool isPause)
        {
            _isPause = isPause;
            OnSwitchPause(isPause);
        }
        protected virtual void OnSwitchPause(bool toPause){}
    }
    #if UNITY_EDITOR
    /// <summary>
    /// エラーを出さないためのエディタ拡張クラス、SKillBaseクラスの派生クラスにも適応される
    /// https://forum.unity.com/threads/nullreferenceexception-serializedobject-of-serializedproperty-has-been-disposed.1431907/
    /// </summary>
    [CustomEditor(typeof(SkillBase), true)]
    public class SkillBaseEditor : Editor
    {
        private SerializedProperty _skillTypeProperty;
        private SerializedProperty _nameProperty;
        private SerializedProperty _explanatoryProperty;
        private SerializedProperty _iconProperty;
        private SerializedProperty _levelUpEventListListProperty;
        private SerializedProperty _skillParamProperty;

        private int _levelUpListLastIndex;
        private void OnEnable()
        {
            _skillTypeProperty = serializedObject.FindProperty("_skillType");
            _nameProperty = serializedObject.FindProperty("_skillName");
            _explanatoryProperty = serializedObject.FindProperty("_explanatoryText");
            _iconProperty = serializedObject.FindProperty("_skillIcon");
            _levelUpEventListListProperty =
                serializedObject.FindProperty("SkillLevelUpEvent._levelUpType._levelUpEventListList");
            _levelUpListLastIndex = _levelUpEventListListProperty.arraySize - 1;
            _skillParamProperty = serializedObject.FindProperty("_skillParam");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            //スクリプト参照(ScriptableObject) MonoBehaviourは別の方法で取得
            using(new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUILayout.ObjectField("Script", MonoScript.FromScriptableObject((ScriptableObject)target), typeof(ScriptableObject), false);
            }
            
            //enumの入力
            _skillTypeProperty.enumValueIndex = 
                (int)(PlayerSkill)EditorGUILayout.EnumPopup("スキルタイプ", (PlayerSkill)_skillTypeProperty.enumValueIndex);
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("-----スキル情報-----");
            EditorGUILayout.PropertyField(_nameProperty, new GUIContent("スキル名"), true);
            EditorGUILayout.LabelField("スキル説明文");
            _explanatoryProperty.stringValue =
                EditorGUILayout.TextArea(_explanatoryProperty.stringValue);
            EditorGUILayout.PropertyField(_iconProperty, new GUIContent("スキルアイコン"), true);
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("-----スキル性能-----");
            // ExampleCustomList list = new ExampleCustomList(_levelUpEventListListProperty);
            // list.DoLayoutList();
            EditorGUILayout.PropertyField(_levelUpEventListListProperty, new GUIContent("レベルアップデータ") , true);
            EditorGUILayout.PropertyField(_skillParamProperty , new GUIContent("スキルパラメーター") , true);
            serializedObject.ApplyModifiedProperties();
        }
        
    }
    #endif
}