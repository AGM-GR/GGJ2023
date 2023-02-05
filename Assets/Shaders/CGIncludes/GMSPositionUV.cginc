//Gone Mad Studios -- Position UV

#ifndef GMS_POSITION_UV
#define GMS_POSITION_UV

//---------------------------------------

float _TextureScale;

//-------------------------------------------------------------------------------------
float3 ObjectScale() {
	return float3(
		length(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x)), // scale x axis
		length(float3(unity_ObjectToWorld[0].y, unity_ObjectToWorld[1].y, unity_ObjectToWorld[2].y)), // scale y axis
		length(float3(unity_ObjectToWorld[0].z, unity_ObjectToWorld[1].z, unity_ObjectToWorld[2].z))  // scale z axis
		);
}

float2 LocalPositionTexcoord(float3 localPos, float3 localNormal, float4 texture_ST) {
	float3 objectScale = ObjectScale();

	float2 c1 = localPos.yz * objectScale.yz;
	float2 c2 = localPos.xz * objectScale.xz;
	float2 c3 = localPos.xy * objectScale.xy;
	float2 c21 = lerp(c2, c1, abs(localNormal.x));
	float2 c23 = lerp(c21, c3, abs(localNormal.z));

	return c23 * _TextureScale * texture_ST.xy + texture_ST.zw;
}

float2 LocalPositionTexcoordUnscaled(float3 localPos, float3 localNormal, float4 texture_ST) {
	float3 objectScale = ObjectScale();

	float2 c1 = localPos.yz * objectScale.yz;
	float2 c2 = localPos.xz * objectScale.xz;
	float2 c3 = localPos.xy * objectScale.xy;
	float2 c21 = lerp(c2, c1, abs(localNormal.x));
	float2 c23 = lerp(c21, c3, abs(localNormal.z));

	return c23 * texture_ST.xy + texture_ST.zw;
}

float2 WorldPositionTexcoord(float3 worldPos, float3 worldNormal, float4 texture_ST) {
	float2 c1 = worldPos.yz;
	float2 c2 = worldPos.xz;
	float2 c3 = worldPos.xy;
	float2 c21 = lerp(c2, c1, abs(worldNormal.x));
	float2 c23 = lerp(c21, c3, abs(worldNormal.z));

	return c23 * _TextureScale * texture_ST.xy + texture_ST.zw;
}

float2 WorldPositionTexcoordUnscaled(float3 worldPos, float3 worldNormal, float4 texture_ST) {
	float2 c1 = worldPos.xy;
	float2 c2 = worldPos.zy;
	float2 c3 = worldPos.zx;
	float2 c21 = lerp(c2, c1, abs(worldNormal.z));
	float2 c23 = lerp(c21, c3, abs(worldNormal.y));

	return c23 * texture_ST.xy + texture_ST.zw;
}

#endif