using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using Unity.VisualScripting;
using UnityEditor;
#endif
namespace SoulRunProject.Common
{


    /// <summary>
    /// Showing an array with Enum as keys in the property inspector. (Supported children)
    /// </summary>
    public class EnumDrawerAttribute : PropertyAttribute {
        private readonly string[] _names;
        
        public EnumDrawerAttribute(Type enumType)
        {
            _names = Enum.GetNames(enumType);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Show inspector
        /// </summary>
        [CustomPropertyDrawer(typeof(EnumDrawerAttribute))]
        private class Drawer : PropertyDrawer 
        {
            public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
            {
                
                var names = ((EnumDrawerAttribute)attribute)._names;
                var index = int.Parse(property.propertyPath.Split('[', ']').Last(c => !string.IsNullOrEmpty(c)));
                
                if (index < names.Length)
                {
                    label.text = names[index];
                }
                EditorGUI.PropertyField(rect, property, label, includeChildren: true);
                //property.isExpanded = true;
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
            {
                return EditorGUI.GetPropertyHeight(property, label, includeChildren: true);
            }
        }
#endif
    }
}