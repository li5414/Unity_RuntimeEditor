// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Battlehub/RTGizmos/Handles" {
	Properties
	{
		_Color("Color", Color) = (0,0,0,1)
		_Scale("Scale", Float) = 1.0
		_ZWrite("__zw", Float) = 0.0
		_ZTest("__zt", Float) = 0.0
		_VertexOffset("VertexOffset", Float) = 0.0
		_Offset("_Offset", Float) = 0.0
		_MinAlpha("_MinAlpha", Float) = 0.2
	
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent+2" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Back
			ZTest [_ZTest]
			ZWrite [_ZWrite]
			Offset [_Offset], [_Offset]
	
			CGPROGRAM

			#include "UnityCG.cginc"
			#pragma vertex vert  
			#pragma fragment frag 
   

			struct vertexInput {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 offset : TEXCOORD0;
				float4 color: COLOR;
			};
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 color: COLOR;
			};

			float _Scale;
			fixed4 _Color;
			float _VertexOffset;
			float _MinAlpha;

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				
				float3 viewpos = lerp(-UnityObjectToViewPos(input.vertex.xyz), float3(0, 0, 1), unity_OrthoParams.w);
				float3 viewNorm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, input.normal));
				float dotNorm = dot(viewNorm, viewpos);
				float s = step(0.0f, dotNorm);

				float t = unity_CameraProjection[1].y;
				float h = _ScreenParams.y;
				float orthoSize = unity_OrthoParams.y;

				float4 vert = float4(UnityObjectToViewPos(input.vertex.xyz), 1.0);
				vert += float4(0, 0, _VertexOffset, 0);

				float dist = dot(vert.xyz, float3(0, 0, 1));
				float tan = 1.0f / t;
				float denom = lerp(dist * 6 * _Scale / h * tan, orthoSize * 6.0 * _Scale / h, unity_OrthoParams.w);
				//denom *= lerp(0.93, 1.0, dotNorm * 0.5 + 0.5);

				output.pos = mul(UNITY_MATRIX_P, vert -
					float4(input.offset.x * denom, input.offset.y * denom, 0.0, 0.0));

				output.color = input.color;
				output.color.a = max(_MinAlpha, s);

				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				return input.color * _Color;
			}

			ENDCG
		}
	}
}