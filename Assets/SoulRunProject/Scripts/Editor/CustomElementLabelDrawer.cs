using System.Linq;
using SoulRunProject.Runtime;
using UnityEditor;
using UnityEngine;

namespace SoulRunProject.EditorExtension
{
    [CustomPropertyDrawer(typeof(CustomElementLabelAttribute))]
    public class CustomElementLabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            var index = int.Parse(property.propertyPath.Split('[', ']').Last(c => !string.IsNullOrEmpty(c)));
            var attr = (CustomElementLabelAttribute)attribute;
            EditorGUI.PropertyField(rect, property, new GUIContent($"{attr.Name} {index + attr.AddNumber}"), true);
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property, label, includeChildren: true);
        }
    }
}