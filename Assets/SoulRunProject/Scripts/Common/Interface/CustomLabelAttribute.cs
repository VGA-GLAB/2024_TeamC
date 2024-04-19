using UnityEngine;
#if  UNITY_EDITOR
using UnityEditor;
#endif 
namespace SoulRunProject.Common
{


    public class CustomLabelAttribute : PropertyAttribute
    {
        public readonly GUIContent Label;//GUIContent型に変更
        public CustomLabelAttribute(string label){
            Label = new GUIContent(label);//stringからGUIContentに変換
        }
    }

#if  UNITY_EDITOR
//カスタムアトリビュートに関連づけられたプロパティドロワーの宣言
    [CustomPropertyDrawer(typeof(CustomLabelAttribute))]
    public class CustomLabelAttributeDrawer : PropertyDrawer
    {
        //エディタ上でカスタムプロパティを描画
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
            //カスタムアトリビュートをCustomLabelAttributeとして取得
            var newLabel = attribute as CustomLabelAttribute;
            //カスタムアトリビュートのラベルをプロパティのラベルに設定
            label = newLabel.Label;
            //エディタ上にプロパティを描画
            EditorGUI.PropertyField(position, property, label, true);
        }

        //エディタ上でプロパティの高さを取得
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label){
            //プロパティの高さを取得
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
#endif 
}