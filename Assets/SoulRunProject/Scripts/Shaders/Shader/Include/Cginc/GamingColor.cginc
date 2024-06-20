#ifndef GAMINGCOLOR_INCLUDED
#define GAMINGCOLOR_INCLUDED

float3 ComputeGamingColor(float time, float2 uv, float scale, float cycle, float freqR, float freqG, float freqB, float red, float green, float blue, float2 direction)
{
    float PI = 3.14159265;
    float xy = dot(uv, direction) * scale;
    return float3(
        red * (sin(freqR * (time + xy) / cycle) + 1) / 2,
        green * (sin(freqG * (time + xy) / cycle + PI * 2 / 3) + 1) / 2,
        blue * (sin(freqB * (time + xy) / cycle + PI * 4 / 3) + 1) / 2
    );
}

#endif // GAMINGCOLOR_INCLUDED