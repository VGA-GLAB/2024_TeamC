Shader "HikanyanLaboratory/UIFadeAlphaShader"
{
    Properties
    {
        [PerRendererData] _MaskTex("Mask Texture", 2D) = "white" {}
        [PerRendererData] _Color ("Tint", Color) = (1,1,1,1)
        [PerRendererData] _Range("Range", Range (0, 1)) = 0
    }
    SubShader
    {
        Tags
        {
            "Queue"="AlphaTest"
            "IgnoreProjector"="True"
            "RenderType"="TransparentCutout"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            #include "Assets/SoulRunProject/Scripts/Shaders/Shader/Include/Cginc/core.cginc"
            #include "Assets/SoulRunProject/Scripts/Shaders/Shader/Include/Cginc/vert.cginc"
            #include "Assets/SoulRunProject/Scripts/Shaders/Shader/Include/Cginc/frag.cginc"
            ENDCG

        }
    }
}