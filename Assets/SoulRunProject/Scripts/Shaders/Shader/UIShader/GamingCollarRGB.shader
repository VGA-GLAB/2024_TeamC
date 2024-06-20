Shader "Custom/GamingRGB"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [MaterialToggle] _Decol ("Decolor", Float ) = 1
        [HDR]_BaseColor ("Base Color", Color) = (1,1,1,1)
        _Red ("Red", Range(0, 1)) = 1
        _Green ("Green", Range(0, 1)) = 1
        _Blue ("Blue", Range(0, 1)) = 1
        _GradationSpeed ("Gradation Speed", Range(-100.00, 100.00)) = 1.0
        _GradationDirection ("Gradation Direction", Vector) = (1,1,0,0)
        _Cycle ("Cycle", Range(0.01, 1.00)) = 0.1
        _Scale ("Scale", Range(0.01, 10.00)) = 1
        _RGBIntensity ("RGB_Intensity", Range(0, 1)) = 1
        _TexIntensity ("Texture_Intensity", Range(0, 1)) = 1
        _FreqR ("Freq_R", Range(0.1, 5.0)) = 1
        _FreqG ("Freq_G", Range(0.1, 5.0)) = 1
        _FreqB ("Freq_B", Range(0.1, 5.0)) = 1
        _Cutoff("Cutoff", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags
        {
            "Queue" = "AlphaTest"
            "RenderType" = "TransparentCutoff"
        }
        LOD 100

        Pass
        {
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Assets/SoulRunProject/Scripts/Shaders/Shader/Include/Cginc/GamingColor.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Decol;
            fixed4 _BaseColor;
            fixed4 _GradationDirection;
            float _Red;
            float _Green;
            float _Blue;
            float _GradationSpeed;
            float _Cycle;
            float _Scale;
            float _RGBIntensity;
            float _TexIntensity;
            float _FreqR;
            float _FreqG;
            float _FreqB;
            float _Cutoff;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 direction = normalize(_GradationDirection.xy);
                 float3 gaming_col = ComputeGamingColor(_Time.y * _GradationSpeed, i.uv, _Scale, _Cycle, _FreqR, _FreqG, _FreqB, _Red, _Green, _Blue, direction);
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed ave = (col.x + col.y + col.z) / 3;
                fixed4 baseCol = _BaseColor * (1 - _Decol) + fixed4(ave, ave, ave, 1) * _Decol;
                fixed4 o = clamp(
                    baseCol * _TexIntensity +
                    fixed4(gaming_col * _RGBIntensity, 1),
                    0, 1);
                clip(col.a - _Cutoff);
                return o;
            }
            ENDCG
        }
    }
}