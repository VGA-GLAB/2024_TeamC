Shader "HikanyanLaboratory/URP/DissolveShader"
{
    Properties
    {
        [HDR] _BaseColor ("Color", Color) = (1,1,1)
        [HDR] _EdgeColor ("Dissolve Color", Color) = (0, 0, 0)
        _MainTex ("Texture", 2D) = "white" {}
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        _AlphaClipThreshold ("Alpha Clip Threshold", Range(0,1)) = 0.5
        _EdgeWidth ("Disolve Margin Width", Range(0,1)) = 0.01
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _DissolveTex;
            float4 _DissolveTex_ST;
            fixed4 _BaseColor;
            fixed4 _EdgeColor;
            half _AlphaClipThreshold;
            half _EdgeWidth;


            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 edgeCol = fixed4(1, 1, 1, 1);

                // noise textureからalpha値を取得
                fixed4 dissolve = tex2D(_DissolveTex, i.uv);
                float alpha = dissolve.r * 0.2 + dissolve.g * 0.7 + dissolve.b * 0.1;

                // dissolveを段階的な色変化によって実現する
                if (alpha < _AlphaClipThreshold + _EdgeWidth && _AlphaClipThreshold > 0)
                {
                    edgeCol = _EdgeColor;
                }
                if (alpha < _AlphaClipThreshold)
                {
                    discard;
                }

                fixed4 col = tex2D(_MainTex, i.uv) * _BaseColor * edgeCol;
                return col;
            }
            ENDCG
        }
    }
}