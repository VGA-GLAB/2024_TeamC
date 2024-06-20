Shader "HikanyanLaboratory/EndlessGround"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        Cull Off
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog // make fog work

            #include "UnityCG.cginc"

            struct appdata
            {
                fixed4 vertex : POSITION;
                fixed2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            fixed4 _MainTex_ST;

            // https://briansharpe.wordpress.com/2011/11/15/a-fast-and-simple-32bit-floating-point-hash-function/
            float4 hash4fast(float2 gridcell)
            {
                const float2 OFFSET = float2(26.0, 161.0);
                const float DOMAIN = 71.0;
                const float SOMELARGEFIXED = 951.135664;
                float4 P = float4(gridcell.xy, gridcell.xy + 1);
                P = frac(P * (1 / DOMAIN)) * DOMAIN;
                P += OFFSET.xyxy;
                P *= P;
                return frac(P.xzxz * P.yyww * (1 / SOMELARGEFIXED));
            }
            

            v2f vert(appdata v)
            {
                float3 forward = -UNITY_MATRIX_V._m20_m21_m22;
                float3 campos = _WorldSpaceCameraPos;
                float center_distance = abs(_ProjectionParams.z - _ProjectionParams.y) * 0.5;
                float3 center = campos + forward * (center_distance + abs(_ProjectionParams.y));
                float3 pos = float3(v.vertex.x * center_distance * 0.5 + center.x * 0.2,
                                          0, // ground level
                                          v.vertex.z * center_distance * 0.5 + center.z * 0.2);
                v2f o;
                //o.vertex = UnityWorldToClipPos(pos);
                o.vertex = UnityObjectToClipPos(pos);
                o.uv = TRANSFORM_TEX(pos.xz*float2(1.0/16.0, 1.0/16.0), _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 off = hash4fast(floor(i.uv));
                off.zw = off.zw >= float2(0.5, 0.5) ? float2(1, 1) : float2(-1, -1);
                float2 fuv = frac(i.uv);
                float2 uv = fuv * off.zw + off.xy;
                float2 dx = ddx(i.uv) * off.zw;
                float2 dy = ddy(i.uv) * off.zw;
                fixed4 col = tex2Dgrad(_MainTex, uv, dx, dy);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}