using System;
using System.Linq;
using SoulRunProject.Runtime;
using UnityEditor;
using UnityEngine;

namespace SoulRunProject.EditorExtension
{
    [CustomPropertyDrawer(typeof(ShowWhenEnumAttribute))]
    internal sealed class ShowWhenEnumDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as ShowWhenEnumAttribute;
            var selfName = property.propertyPath.Split('.').Last();
            var conditionPath = property.propertyPath.Replace(selfName, "");
            conditionPath += attr.EnumVariableName;
            var conditionProperty = property.serializedObject.FindProperty(conditionPath);
            using (new EditorGUI.PropertyScope(position, label, property))
            {
                if(conditionProperty == null)
                {
                    Debug.LogError($"Not found '{attr.EnumVariableName}' property");
                    if(property.hasVisibleChildren) property.NextVisible(true);
                    EditorGUI.PropertyField(position, property, label, true);
                    return;
                }
                if(conditionProperty.propertyType == SerializedPropertyType.Enum)
                {
                    string enumValue = Enum.GetValues(attr.EnumValue.GetType())
                        .GetValue(conditionProperty.enumValueIndex).ToString();
                    if (attr.EnumValue.ToString() == enumValue)
                    {
                        if(property.hasVisibleChildren) property.NextVisible(true);
                        EditorGUI.PropertyField(position, property, label, true);
                    }
                }
                else
                {
                    Debug.LogError($"'{attr.EnumVariableName}' property is not enum");
                    if(property.hasVisibleChildren) property.NextVisible(true);
                    EditorGUI.PropertyField(position, property, label, true);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attr = attribute as ShowWhenEnumAttribute;
            var selfName = property.propertyPath.Split('.').Last();
            var conditionPath = property.propertyPath.Replace(selfName, "");
            conditionPath += attr.EnumVariableName;
            var conditionProperty = property.serializedObject.FindProperty(conditionPath);
            if(conditionProperty.propertyType == SerializedPropertyType.Enum)
            {
                string enumValue = Enum.GetValues(attr.EnumValue.GetType())
                    .GetValue(conditionProperty.enumValueIndex).ToString();
                if (attr.EnumValue.ToString() != enumValue)
                {
                    return -EditorGUIUtility.standardVerticalSpacing;
                }
            }
            if(property.hasVisibleChildren) property.NextVisible(true);
            return EditorGUI.GetPropertyHeight(property, true);
        }

        private bool IsEnum(object obj)
        {
            return obj != null && obj.GetType().IsEnum;
        }
    }
}