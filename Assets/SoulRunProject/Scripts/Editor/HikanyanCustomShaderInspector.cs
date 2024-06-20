using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEditor;

public class HikanyanCustomShaderGUI : ShaderGUI
{
    private readonly List<FieldInfo> _properties;
    private int[] _foldoutStatuses;
    private bool _isInitialized;


    private MaterialProperty _mainTexture;
    private MaterialProperty _mainColor;
    private MaterialProperty _outlineColor;
    private MaterialProperty _outlineWidth;
    private MaterialProperty _matCap;
    private MaterialProperty _rimColor;
    private MaterialProperty _rimPower;
    private MaterialProperty _emissionTex;
    private MaterialProperty _emissionColor;
    private MaterialProperty _emissionStrength;
    private MaterialProperty _foldoutStatus1;
    private MaterialProperty _foldoutStatus2;

    // public HikanyanCustomShaderGUI()
    // {
    //     _isInitialized = false;
    //     _properties = typeof(HikanyanCustomShaderGUI).GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
    //         .Where(w => w.FieldType == typeof(MaterialProperty)).ToList();
    // }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        // var material = (Material)materialEditor.target;
        // InjectMaterialProperties(properties);
        // OnHeaderGui("Hikanyan Custom Shader");
        // OnInitialize(material);
        // OnInitializeFoldout(_foldoutStatus1, _foldoutStatus2);
        //
        // OnMainColor(materialEditor);
        //
        // OnStoreFoldout(_foldoutStatus1, _foldoutStatus2);
    }

    void OnMainColor(MaterialEditor materialEditor)
    {
        OnFoldOutGui(Category.Color, () =>
        {
            materialEditor.TextureProperty(_mainTexture, "Texture");
            materialEditor.ColorProperty(_mainColor, "Main Color");
            materialEditor.ColorProperty(_outlineColor, "Outline Color");
            materialEditor.FloatProperty(_outlineWidth, "Outline Width");
            materialEditor.TextureProperty(_matCap, "MatCap Texture");
            materialEditor.ColorProperty(_rimColor, "Rim Light Color");
            materialEditor.FloatProperty(_rimPower, "Rim Power");
            materialEditor.TextureProperty(_emissionTex, "Emission Texture");
            materialEditor.ColorProperty(_emissionColor, "Emission Color");
            materialEditor.FloatProperty(_emissionStrength, "Emission Strength");
        });
    }
    
    
    // シェーダーのプロパティをインスペクターに注入する
    void InjectMaterialProperties(MaterialProperty[] properties)
    {
        foreach (var property in _properties)
        {
            var value = FindProperty(property.Name, properties);
            property.SetValue(this, value);
        }
    }
    
    // ヘッダーセクションのGUIを描画
    private void OnHeaderGui(string title)
    {
        EditorStyles.label.wordWrap = true;
        using (new Section(title))
            EditorGUILayout.LabelField($"{title} - Part of Sakura Shader by Natsuneko");
        EditorStyles.label.wordWrap = false;
    }
    
    private void OnInitialize(Material material)
    {
        if (_isInitialized)
            return;
        _isInitialized = true;
    
        foreach (var keyword in material.shaderKeywords)
            material.DisableKeyword(keyword);
    }
    
    private void OnInitializeFoldout(params MaterialProperty[] foldout)
    {
        _foldoutStatuses = foldout.Select(w => (int)w.floatValue).ToArray();
    }
    
    protected void OnStoreFoldout(params MaterialProperty[] foldout)
    {
        if (foldout.Length != _foldoutStatuses.Length)
            return;
    
        for (var i = 0; i < _foldoutStatuses.Length; i++)
            foldout[i].floatValue = _foldoutStatuses[i];
    }
    
    void OnFoldOutGui<T>(T category, Action callback) where T : Enum
    {
        var title = typeof(T).GetMember(category.ToString())
                        .Select(w => w.GetCustomAttribute<EnumMemberAttribute>(false)?.Value).FirstOrDefault() ??
                    category.ToString();
        var style = new GUIStyle("ShurikenModuleTitle")
        {
            border = new RectOffset(15, 7, 4, 4),
            fontSize = 12,
            fixedHeight = 24,
            contentOffset = new Vector2(20, -2)
        };
    
        var rect = GUILayoutUtility.GetRect(16.0f, 22.0f, GUIStyle.none);
        GUI.Box(rect, title, style);
    
        var e = Event.current;
        var foldoutState = GetFoldState((int)(object)category);
        var foldoutRect = new Rect(rect.x + 4, rect.y + 2, 16, 16);
        if (e.type == EventType.Repaint)
            EditorStyles.foldout.Draw(foldoutRect, false, false, foldoutState, false);
    
        if (e.type == EventType.MouseDown)
        {
            var foldoutArea = new Rect(0, rect.y + 2f, rect.width, 16);
            if (foldoutArea.Contains(e.mousePosition))
            {
                SetFoldState((int)(object)category, !foldoutState);
                e.Use();
            }
        }
    
        if (GetFoldState((int)(object)category))
        {
            using (new EditorGUI.IndentLevelScope())
                callback.Invoke();
    
            EditorGUILayout.Space();
        }
    }
    
    bool GetFoldState(int category)
    {
        var index = category / 32;
        return (_foldoutStatuses[index] & (1 << category)) > 0;
    }
    
    void SetFoldState(int category, bool value)
    {
        var index = category / 32;
        if (value)
            _foldoutStatuses[index] |= 1 << category;
        else
            _foldoutStatuses[index] &= ~(1 << category);
    }
    
    private class Section : IDisposable
    {
        private readonly IDisposable _disposable;
    
        public Section(string title)
        {
            GUILayout.Label(title, EditorStyles.boldLabel);
            _disposable = new EditorGUILayout.VerticalScope(GUI.skin.box);
        }
    
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
    
    private enum Category
    {
        [EnumMember(Value = "Main Color")] Color = 1,
    
        Stencil,
    
        [EnumMember(Value = "1st Shape")] Shape1,
    
        [EnumMember(Value = "2nd Shape")] Shape2,
    
        [EnumMember(Value = "3rd Shape")] Shape3,
    
        [EnumMember(Value = "4th Shape")] Shape4,
    
        [EnumMember(Value = "5th Shape")] Shape5,
    }
}