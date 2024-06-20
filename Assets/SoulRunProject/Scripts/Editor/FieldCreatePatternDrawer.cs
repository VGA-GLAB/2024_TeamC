using SoulRunProject.InGame;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace SoulRunProject.EditorExtension
{
    // [CustomPropertyDrawer(typeof(FieldCreatePattern))]
    // public class FieldCreatePatternDrawer : PropertyDrawer
    // {
    //     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //     {
    //         if (property.isArray)
    //         {
    //             Debug.Log(property.propertyPath);
    //             Debug.Log(property.GetArrayElementAtIndex(0).propertyPath);
    //         }
    //         // var serializedMode = property.FindPropertyRelative("_mode");
    //         // EditorGUILayout.PropertyField(serializedMode);
    //         // EditorGUILayout.PropertyField(property.FindPropertyRelative("_seconds"));
    //         // switch (serializedMode.GetEnumValue<FieldMoverMode>())
    //         // {
    //         //    case FieldMoverMode.Order:
    //         //        EditorGUILayout.PropertyField(property.FindPropertyRelative("_fieldSegments"));
    //         //        break;
    //         //    case FieldMoverMode.Random:
    //         //        EditorGUILayout.PropertyField(property.FindPropertyRelative("_adjacentGraph"));
    //         //        EditorGUILayout.PropertyField(property.FindPropertyRelative("_randomFirstSegment"));
    //         //        EditorGUILayout.PropertyField(property.FindPropertyRelative("_randomLastSegment"));
    //         //        break;
    //         // }
    //     }
    // }
}