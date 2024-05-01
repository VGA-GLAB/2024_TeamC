using System;
using SoulRunProject.InGame;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace SoulRunProject.EditorExtension
{
    #if UNITY_EDITOR
    [CustomEditor(typeof(FieldMover))]
    public class FieldMoverEditor : Editor
    {
        [SerializeField] VisualTreeAsset _fieldMoverVisual;
        [SerializeField] VisualTreeAsset _fieldCreatePatternVisual;
        const string FieldMoverVisualPath = "Assets/SoulRunProject/Scripts/UIToolKit/UXML/FieldMoverEditor.uxml";
        const string FieldCreatePatternVisualPath = "Assets/SoulRunProject/Scripts/UIToolKit/UXML/FieldCreatePatternDrawer.uxml";
        
        public override VisualElement CreateInspectorGUI()
        {
            //  コンパイルするとMonoScriptに設定していたVisualTreeAssetがnullになるのでnullの時はパスで参照を受け取るようにした
            var root = (_fieldMoverVisual
                ? _fieldMoverVisual
                : AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(FieldMoverVisualPath)).CloneTree();
            var listView = root.Q<ListView>();
            var monoScript = root.Q<ObjectField>("MonoScript");
            monoScript.value = MonoScript.FromMonoBehaviour((MonoBehaviour)target);
            monoScript.SetEnabled(false);
            
            listView.makeItem = ()=>
            {
                //  コンパイルするとMonoScriptに設定していたVisualTreeAssetがnullになるのでnullの時はパスで参照を受け取るようにした
                var pattern = (_fieldCreatePatternVisual
                    ? _fieldCreatePatternVisual
                    : AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(FieldCreatePatternVisualPath)).CloneTree();
                var normalGroup = pattern.Q<GroupBox>("NormalGroup");
                var randomGroup = pattern.Q<GroupBox>("RandomGroup");
                var bossGroup = pattern.Q<GroupBox>("BossGroup");
                var dropdown = pattern.Q<EnumField>();
                dropdown.hierarchy[1].pickingMode = PickingMode.Position;
                SwitchGroup(dropdown.value, normalGroup, randomGroup, bossGroup);
                dropdown.RegisterValueChangedCallback(c => SwitchGroup(c.newValue,normalGroup, randomGroup, bossGroup));
                return pattern;
            };
            
            return root;
        }

        void SwitchGroup(Enum mode, GroupBox normalGroup, GroupBox randomGroup, GroupBox bossGroup)
        {
            switch (mode)
            {
                case FieldMoverMode.Normal:
                    normalGroup.style.display = DisplayStyle.Flex;
                    randomGroup.style.display = DisplayStyle.None;
                    bossGroup.style.display = DisplayStyle.None;
                    break;
                case FieldMoverMode.Random:
                    normalGroup.style.display = DisplayStyle.None;
                    randomGroup.style.display = DisplayStyle.Flex;
                    bossGroup.style.display = DisplayStyle.None;
                    break;
                case FieldMoverMode.Boss:
                    normalGroup.style.display = DisplayStyle.None;
                    randomGroup.style.display = DisplayStyle.None;
                    bossGroup.style.display = DisplayStyle.Flex;
                    break;
            }
        }
    }
    #endif
}
