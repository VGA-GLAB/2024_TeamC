using System.Linq;
using SoulRunProject.Runtime;
using UnityEngine;
using UnityEditor;

namespace SoulRunProject.EditorExtension
{
    [CustomPropertyDrawer(typeof(ShowWhenBooleanAttribute))]
    internal sealed class ShowWhenBooleanDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as ShowWhenBooleanAttribute;
            var selfName = property.propertyPath.Split('.').Last();
            var conditionPath = property.propertyPath.Replace(selfName, "");
            conditionPath += attr.BoolVariableName;
            var prop = property.serializedObject.FindProperty(conditionPath);
            if(prop == null)
            {
                Debug.LogError($"Not found '{attr.BoolVariableName}' property");
                if(property.hasVisibleChildren) property.NextVisible(true);
                EditorGUI.PropertyField(position, property, label, true);
            }
            if(IsDisable(attr, prop))
            {
                return;
            }
            if(property.hasVisibleChildren) property.NextVisible(true);
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attr = attribute as ShowWhenBooleanAttribute;
            var selfName = property.propertyPath.Split('.').Last();
            var conditionPath = property.propertyPath.Replace(selfName, "");
            conditionPath += attr.BoolVariableName;
            var prop = property.serializedObject.FindProperty(conditionPath);
            if(IsDisable(attr, prop))
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
            if(property.hasVisibleChildren) property.NextVisible(true);
            return EditorGUI.GetPropertyHeight(property, true);
        }

        private bool IsDisable(ShowWhenBooleanAttribute attr, SerializedProperty prop)
        {
            return attr.TrueThenDisable ? prop.boolValue : !prop.boolValue;
        }
    }
}