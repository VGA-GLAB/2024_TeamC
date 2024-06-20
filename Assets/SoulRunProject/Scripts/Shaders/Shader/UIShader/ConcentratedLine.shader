Shader "HikanyanLaboratory/ConcentratedLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]_Color ("Color", Color) = (0, 0, 0, 1)
        [Toggle(IS_GAMEING_COLOR)]_IsGamingColor ("IsGameingColor", Float) = 0
        _RollSpeed ("RollSpeed", float) = 1
        _Density ("Density", float) = 20
        _InnerSpace ("Inner Space", Range(0, 1.0)) = 0
        _Taper("Taper", Range(0, 5.0)) = 0
        _OuterEdge ("OuterEdge", Range(0, 1.00)) = 0.1
        _Offset ("Offset", Range(-5, 5)) = 0.5
        _ThresholdL ("ThresholdL", Range(-3, 3)) = 0.04
        _ThresholdH ("ThresholdH", Range(-3, 3)) = 0.07

    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True"
        }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            Name "BASE"
            Cull Off

            CGPROGRAM
            #pragma shader_feature IS_GAMEING_COLOR
            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile_fog

            #include "UnityCG.cginc"


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
            float _RollSpeed;
            float _RollSpeed2;
            float _Density;
            float _InnerSpace;
            float _Taper;
            float _OuterEdge;
            float _Offset;
            float _ThresholdL;
            float _ThresholdH;
            fixed4 _Color;

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
                const float PI = 3.141592653589793;

                // 画像の中心からの角度を求める
                float uvCenterOffset = atan((i.uv.x - 0.5) / (i.uv.y - 0.5));

                // 波の計算
                float wave1 = cos(uvCenterOffset * 4 * _Density + _Time.x * _RollSpeed * 7) * 0.8;
                wave1 += cos(uvCenterOffset * 14 * _Density + _Time.x * _RollSpeed * 3) * 0.4;
                wave1 += cos(uvCenterOffset * 38 * _Density + _Time.x * _RollSpeed * 1) * 0.2;

                // 波の計算2
                float wave2 = cos(uvCenterOffset * 6 * _Density - _Time.x * _RollSpeed * 9) * 0.8;
                wave1 += cos(uvCenterOffset * 12 * _Density - _Time.x * _RollSpeed * 5) * 0.4;
                wave1 += cos(uvCenterOffset * 40 * _Density - _Time.x * _RollSpeed * 3) * 0.2;

                float r = sqrt(pow((i.uv.x - 0.5), 2) + pow((i.uv.y - 0.5), 2)); // 中心からの距離
                // 線の濃さ
                float spmod = (r < _InnerSpace) ? 0 : ((r - _InnerSpace) * _Taper); // 中心部の空白を確保し外に行くほど濃くする

                // アルファ値の調整
                float alpmod = (r < _OuterEdge) ? 1 : ((0.5 - r) * (1 / (0.5 - _OuterEdge))); // 外側エッジを超えたら徐々にフェードアウトさせる
                alpmod = clamp(alpmod, 0, 1);

                float combinedWave = ((wave1 + wave2) + _Offset) * spmod;

                // ThresholdHとLの処理
                combinedWave = (combinedWave < _ThresholdL)
                                   ? 0
                                   : (combinedWave > _ThresholdH)
                                   ? 1
                                   : ((combinedWave - _ThresholdL) * (1 / (_ThresholdH - _ThresholdL)));

                #ifdef IS_GAMEING_COLOR
                // ゲーミング
                float t = fmod(_Time.z * _RollSpeed2 + (a / 2 * PI) * 360 + 720, 360);
                _Color = (t < 60)
                                     ? fixed4(1, t / 60, 0, 1)
                                     : (t < 120)
                                     ? fixed4(1 - (t - 60) / 60, 1, 0, 1)
                                     : (t < 180)
                                     ? fixed4(0, 1, (t - 120) / 60, 1)
                                     : (t < 240)
                                     ? fixed4(0, 1 - (t - 180) / 60, 1, 1)
                                     : (t < 300)
                                     ? fixed4((t - 240) / 60, 0, 1, 1)
                                     : (t < 360)
                                     ? fixed4(1, 0, 1 - (t - 300) / 60, 1)
                                     : fixed4(0, 0, 0, 0);
                #endif

                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                col.a = col.a * combinedWave * alpmod;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }

    Fallback "VertexLit"
}