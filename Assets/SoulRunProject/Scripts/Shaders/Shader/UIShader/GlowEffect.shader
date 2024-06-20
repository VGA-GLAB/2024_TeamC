Shader "HikanyanLaboratory/GlowEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]_GlowColor ("Glow Color", Color) = (1,1,1,1)
        _GlowAmount ("Glow Amount", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
        }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _GlowColor;
            float _GlowAmount;


            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // シルエット
                fixed4 silhouetteColor = i.color; // シルエットの色
                silhouetteColor.a *= col.a; // 保持するアルファ値

                // グロー
                half4 glow = _GlowColor * _GlowAmount;
                glow *= silhouetteColor.a;

                // 元のテクスチャを最終的な出力として使用
                fixed4 finalColor = col;
                finalColor.rgb += glow.rgb;

                finalColor.rgb = lerp(finalColor.rgb, _GlowColor.rgb, _GlowAmount);

                return finalColor;
            }
            ENDCG
        }
    }
}