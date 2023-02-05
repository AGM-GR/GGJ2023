//Gone Mad Studios -- Dissolve solution

#ifndef GMS_DISSOLVE_INCLUDED
#define GMS_DISSOLVE_INCLUDED

#include "GMSPositionUV.cginc"

//---------------------------------------

sampler2D	_DissolveTex;
float4		_DissolveTex_ST;

float		_DissolveSize;
float		_DissolveProgression;
half4       _DissolveGlow;
float		_DissolveGlowTightness;
half		_DissolveShadow;
half		_DissolveWorldSpace;

//-------------------------------------------------------------------------------------
// Input functions

half Dissolve(float2 uv) {
	//Convierte DissolveProgression a escala -1 a 1
	half dissolveValue = -2 * _DissolveProgression + 1;
	//Lee el canal rojo de la textura de disolucion añadiendole el valor de la progresión
	half dissolveTexRead = tex2D(_DissolveTex, uv * _DissolveSize * _DissolveTex_ST.xy + _DissolveTex_ST.zw).r + dissolveValue;
	return dissolveTexRead;
}

half2 LocalCoordDissolve(float3 localPos, float3 localNormal) {
	return LocalPositionTexcoordUnscaled(localPos, localNormal, _DissolveTex_ST);
}

half2 WorldCoordDissolve(float3 localPos, float3 localNormal) {
	return WorldPositionTexcoordUnscaled(localPos, localNormal, _DissolveTex_ST);
}

half ClampDissolve(float2 uv) {
	return clamp(Dissolve(uv), 0.0f, 1.0f) - 0.01;
}

half4 DissolveGlow(float2 uv) {
	half4 dissolveGlow = smoothstep(Dissolve(uv) - 0.01, Dissolve(uv) + 0.01, _DissolveGlowTightness) * _DissolveGlow;
	return dissolveGlow;
}

#endif
