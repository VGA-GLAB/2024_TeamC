#pragma once

v2f vert(appdata input)
{
    v2f output;
    output.worldPosition = input.vertex;
    output.vertex = UnityObjectToClipPos(output.worldPosition);

    output.texcoord = input.texcoord;

    #ifdef UNITY_HALF_TEXEL_OFFSET
    OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
    #endif

    output.color = input.color * _Color;
    return output;
}
