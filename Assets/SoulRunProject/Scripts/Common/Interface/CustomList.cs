using UnityEditor;
using UnityEngine;

namespace SoulRunProject.Common.Interface
{
    public class ExampleCustomList
{
    /// <summary>
    /// 高さ
    /// </summary>
    public float Height { get { return _property.isExpanded ? (ArraySize + 1) * LineHeight : LineHeight; } }

    private float ContentHeight { get; } = EditorGUIUtility.singleLineHeight;
    private float LineHeight { get; } = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing + 4;
    private int ArraySize { get { return _property.arraySize; } }
        
    /// <summary>
    /// ヘッダが描画されたときのコールバック
    /// </summary>
    public event System.Action<Rect> drawHeaderCallback;
    /// <summary>
    /// 要素が描画されたときのコールバック
    /// </summary>
    public event System.Action<Rect, int> drawElementCallback;

    private SerializedProperty _property;
        
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="property">配列のプロパティ</param>
    public ExampleCustomList(SerializedProperty property)
    {
        _property = property;
            
        // デフォルトのヘッダ描画処理を登録
        drawHeaderCallback = rect => {
            rect.xMin += 10;
            EditorGUI.PropertyField(rect, property);
        };
        // デフォルトの要素描画処理を登録
        drawElementCallback = (rect, index) => {
            EditorGUI.PropertyField(rect, _property.GetArrayElementAtIndex(index));
                
        };
    }

    /// <summary>
    /// EditorGUILayout環境で描画する
    /// </summary>
    public void DoLayoutList()
    {
        var lastRect = GUILayoutUtility.GetRect(new GUIContent(""), GUIStyle.none);
        var rect = GUILayoutUtility.GetRect(lastRect.width, Height);
        DoList(rect);
    }

    /// <summary>
    /// EditorGUI環境で描画する
    /// </summary>
    public void DoList(Rect position)
    {
        // 色の定義
        var backgroundDefaultColor = GUI.backgroundColor;
        var backgroundLightColor = backgroundDefaultColor;
        var backgroundDarkColor = backgroundLightColor * 0.8f;
        backgroundDarkColor.a = 1;

        var isExpanded = _property.isExpanded;

        // 外枠を描画
        var outlineRect = position;
        position.height = Height;
        GUI.Box(outlineRect, "");
            
        var fieldRect = position;
        fieldRect.height = LineHeight;

        // ヘッダを描画
        var headerRect = fieldRect;
        var plusButtonRect = fieldRect;
        plusButtonRect.xMin = fieldRect.xMax - fieldRect.height;
        headerRect.xMax -= plusButtonRect.width - 1;
        GUI.Box(headerRect, "");
        drawHeaderCallback(GetContentRect(headerRect));
        if (GUI.Button(plusButtonRect, "+", GUI.skin.box)) {
            _property.arraySize ++;
        }

        if (isExpanded) {
            for (int i = 0; i < ArraySize; i++) {
                fieldRect.y += LineHeight;
                    
                // 背景を描画
                var backgroundRect = fieldRect;
                backgroundRect.xMin += 1;
                backgroundRect.xMax -= 1;
                if (ArraySize == i + 1) {
                    backgroundRect.yMax -= 1;
                }
                EditorGUI.DrawRect(backgroundRect, i % 2 == 0 ? backgroundDarkColor : backgroundLightColor);
                // プロパティを描画
                var propertyRect = GetContentRect(fieldRect);
                drawElementCallback(propertyRect, i);

                // マイナスボタンを描画
                var minusButtonRect = fieldRect;
                minusButtonRect.height -= 4;
                minusButtonRect.y += 2;
                minusButtonRect.xMin = minusButtonRect.xMax - minusButtonRect.height - 2;
                minusButtonRect.xMax -= 2;
                if (GUI.Button(minusButtonRect, "X")) {
                    _property.DeleteArrayElementAtIndex(i);
                    break;
                }
            }
        }
    }

    private Rect GetContentRect(Rect fieldRect)
    {
        fieldRect.height = ContentHeight;
        fieldRect.y += (LineHeight - ContentHeight) / 2;
        fieldRect.xMin += 4;
        fieldRect.xMax -= 24;
        return fieldRect;
    }
}
}