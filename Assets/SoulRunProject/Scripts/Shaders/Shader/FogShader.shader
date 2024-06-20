Shader "HikanyanLaboratory/Fog2"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
           #pragma vertex vert
           #pragma fragment frag
            // #pragma multi_compile_fog
           #pragma multi_compile FOG_EXP FOG_EXP2 FOG_LINEAR

           #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                // UNITY_FOG_COORDS(1)
                float fogCoord : TEXCOORD1;
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                // UNITY_TRANSFER_FOG(o, o.vertex);
                UNITY_CALC_FOG_FACTOR(o.vertex.z);
                o.fogCoord.x = unityFogFactor;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = 1;

                // UNITY_APPLY_FOG(i.fogCoord, col);
               #ifdef UNITY_PASS_FORWARDADD
                    col.rgb = lerp(fixed4(0, 0, 0, 0).rgb, col.rgb, saturate(i.fogCoord));
               #else
                    col.rgb = lerp(unity_FogColor.rgb, col.rgb, saturate(i.fogCoord));
               #endif

                return col;
            }
            ENDCG
        }
    }
}