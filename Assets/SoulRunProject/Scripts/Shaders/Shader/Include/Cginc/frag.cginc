fixed4 frag(v2f input) : SV_Target
{
    half4 color = _Color;
    half mask = tex2D(_MaskTex, input.texcoord).a - (-1 + _Range * 2);
    color.a = mask;

    clip(mask - 0.001);

    return color;
}