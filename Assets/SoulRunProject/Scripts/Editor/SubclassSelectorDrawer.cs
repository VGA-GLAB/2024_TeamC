using System;
using System.Reflection;
using System.Linq;
using SoulRunProject.Common;
using UnityEngine;
using UnityEditor;

namespace SoulRunProject.Editor
{
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer
    {
        private bool _initialized = false;
        private Type[] _inheritedTypes;
        private string[] _typePopupNameArray;
        private string[] _typeFullNameArray;
        private int _currentTypeIndex;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference) return;
            if (!_initialized)
            {
                Initialize(property);
                _initialized = true;
            }

            GetCurrentTypeIndex(property.managedReferenceFullTypename);
            int selectedTypeIndex = EditorGUI.Popup(GetPopupPosition(position), _currentTypeIndex, _typePopupNameArray);
            UpdatePropertyToSelectedTypeIndex(property, selectedTypeIndex);
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        private void Initialize(SerializedProperty property)
        {
            SubclassSelectorAttribute utility = (SubclassSelectorAttribute)attribute;
            GetAllInheritedTypes(GetFieldType(property), utility.IsIncludeMono());
            GetInheritedTypeNameArrays();
        }

        private void GetCurrentTypeIndex(string typeFullName)
        {
            _currentTypeIndex = Array.IndexOf(_typeFullNameArray, typeFullName);
        }

        private void GetAllInheritedTypes(Type baseType, bool includeMono)
        {
            Type monoType = typeof(MonoBehaviour);
            _inheritedTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => baseType.IsAssignableFrom(p) && p.IsClass && (!monoType.IsAssignableFrom(p) || includeMono))
                .Prepend(null)
                .ToArray();
        }

        private void GetInheritedTypeNameArrays()
        {
            _typePopupNameArray = _inheritedTypes.Select(type => type == null ? "<null>" : type.ToString()).ToArray();
            _typeFullNameArray = _inheritedTypes.Select(type =>
                    type == null ? "" : string.Format("{0} {1}", type.Assembly.ToString().Split(',')[0], type.FullName))
                .ToArray();
        }

        public static Type GetFieldType(SerializedProperty property)
        {
            string[] fieldTypename = property.managedReferenceFieldTypename.Split(' ');
            var assembly = Assembly.Load(fieldTypename[0]);
            return assembly.GetType(fieldTypename[1]);
        }

        private void UpdatePropertyToSelectedTypeIndex(SerializedProperty property, int selectedTypeIndex)
        {
            if (_currentTypeIndex == selectedTypeIndex) return;
            _currentTypeIndex = selectedTypeIndex;
            Type selectedType = _inheritedTypes[selectedTypeIndex];
            property.managedReferenceValue =
                selectedType == null ? null : Activator.CreateInstance(selectedType);
        }

        private Rect GetPopupPosition(Rect currentPosition)
        {
            Rect popupPosition = new Rect(currentPosition);
            popupPosition.width -= EditorGUIUtility.labelWidth;
            popupPosition.x += EditorGUIUtility.labelWidth;
            popupPosition.height = EditorGUIUtility.singleLineHeight;
            return popupPosition;
        }
    }
}