/*
自作シェーダーをSRP Batcherに対応させたい場合、UnityPerMaterial
というCBUFFER (constant buffer)を定義する必要があります。
https://zenn.dev/r_ngtm/articles/unity-study-srpbatcher
*/

CBUFFER_START(UnityPerMaterial)

fixed4 _Color;
float _TextureSampleAdd;

float _Range;
CBUFFER_END

//テクスチャは重いので外に
sampler2D _MainTex;
sampler2D _MaskTex;

struct appdata
{
    float4 vertex : POSITION;
    float4 color : COLOR;
    float2 texcoord : TEXCOORD0;
};

struct v2f
{
    float4 vertex : SV_POSITION;
    float4 color : COLOR;
    float2 texcoord : TEXCOORD0;
    float4 worldPosition : TEXCOORD1;
};