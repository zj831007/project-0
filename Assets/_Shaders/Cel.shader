// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Mobile/Cel" {
	Properties {
		BaseTex ("Base Tex", 2D) = "white" {}
		SssTex ("SSS Tex", 2D) = "white" {}
		IlmTex ("ILM Tex", 2D) = "white" {}
	}
    SubShader {
		Tags { "RenderType"="Opaque"}
		
		Pass {
			Cull Front
			
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			sampler2D BaseTex;
			
			struct OutlineA2V
			{
				float3 vertex  : POSITION;
				float3 norm : NORMAL;
    			float col : COLOR;
    			float2 uv : TEXCOORD0;
			};
			
			struct OutlineV2F
			{
				float4 pos  : SV_Position;
   	 			float2 uv : TEXCOORD0;
			};
			
			OutlineV2F vert (OutlineA2V In) {
				OutlineV2F Out = (OutlineV2F)0;

				float3 pos = UnityObjectToViewPos(In.vertex);
				float3 norm = UnityObjectToViewPos(In.norm);
				norm.z = -0.5;
				pos = pos + normalize(norm) * In.col * 0.007;
				Out.pos = UnityViewToClipPos(pos);
				return Out;
			}
			
			float4 frag(OutlineV2F In) : SV_Target {
				return float4(lerp(0, tex2D(BaseTex, In.uv).rgb, 0.35), 1);
			}
			
			ENDCG
		}
		
		Pass {
			Tags { "LightMode"="ForwardBase" }
			
			CGPROGRAM
			
			#pragma multi_compile_fwdbase
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			
			sampler2D BaseTex;
			sampler2D SssTex;
			sampler2D IlmTex;
		
			struct ModelA2V
			{
				float3 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 norm : NORMAL;
				float3 col : COLOR;
			};

			struct ModelV2F
			{
				float4 pos : SV_Position;
				float2 uv : TEXCOORD0;
				float3 worldNorm : TEXCOORD1;
				float2 col : TEXCOORD2;
				float3 wolrdPos : TEXCOORD3;
				SHADOW_COORDS(4)
			};
			
			ModelV2F vert (ModelA2V v) {
				ModelV2F Out = (ModelV2F)0;
				
				Out.pos = UnityObjectToClipPos(v.vertex);
				Out.uv = v.uv;
				Out.worldNorm  = UnityObjectToWorldNormal(v.norm);
				Out.wolrdPos = mul(unity_ObjectToWorld, v.vertex);
				Out.col = v.col.gb;
				
				TRANSFER_SHADOW(Out);
				
				return Out;
			}
			
			float4 frag(ModelV2F In) : SV_Target { 
				float3 base = tex2D(BaseTex, In.uv).rgb;
				float3 sss = tex2D(SssTex, In.uv).rgb;
				float4 ilm = tex2D(IlmTex, In.uv);
				float3 norm = normalize(In.worldNorm);

				float3 LightDir = normalize(UnityWorldSpaceLightDir(In.wolrdPos));
				float3 ViewDir = normalize(UnityWorldSpaceViewDir(In.wolrdPos));

				float3 halfDir = normalize(LightDir + ViewDir);

				UNITY_LIGHT_ATTENUATION(atten, In, In.wolrdPos);

				float3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb;

				float litThreshold = 1 - ilm.b * In.col.g;
				float lit = 0.5 + 0.5 * dot(LightDir, norm) * atten;
				float isLit = step(litThreshold, lit);
				float3 tint = saturate(sss + isLit);
				
				float gloss = In.col.r / 10 + 0.9;
				isLit = isLit * step(1, ilm.r);
				float3 specular = isLit * ilm.g * step(0, saturate(dot(halfDir, norm)) - gloss);
				
				float3 col = base * tint * ilm.r + specular;

				return float4(col * (_LightColor0.rgb + ambient), 1);
			}
		
			ENDCG
		}
		UsePass "VertexLit/SHADOWCASTER"
	}
	FallBack "Standard"
}
