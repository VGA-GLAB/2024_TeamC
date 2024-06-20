Shader "HikanyanLaboratory/URP/SilhouetteGlowUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]_GlowColor ("Glow Color", Color) = (1,1,1,1)
        _GlowAmount ("Glow Amount", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        Pass
        {
            Tags { "LightMode"="UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _GlowColor;
            float _GlowAmount;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 元のテクスチャの色を取得
                fixed4 col = tex2D(_MainTex, i.uv);

                // シルエット
                fixed4 silhouetteColor = i.color; // シルエットの色
                silhouetteColor.a *= col.a; // 保持するアルファ値

                // グロー
                half4 glow = _GlowColor * _GlowAmount;
                glow *= silhouetteColor;

                // シルエットとグローを合成
                fixed4 combinedColor = silhouetteColor + glow;
                combinedColor.a = silhouetteColor.a; // アルファ値を保持

                // 最終的に元のテクスチャの色にシルエットとグローを加算
                fixed4 finalColor = col;
                finalColor.rgb += combinedColor.rgb * combinedColor.a; // アルファに基づいて合成

                // Apply fog
                UNITY_APPLY_FOG(i.fogCoord, finalColor);

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Unlit/Texture"
}
