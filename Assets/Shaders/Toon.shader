Shader "Marco/Toon Shader"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}

		[Header(Ambient Settings)]
		[HDR] // ambient color represents the color that bounces offf the surface of objects
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
		_BlendingMaxValue("Blendeing max Range", Range(0.01,0.2)) = 0.01
		
		[Header(Specular Reflection)]
		[HDR]  // specular reflection
		_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
		_Glossiness("Glossiness", Float) = 32

		[Header(Rim Lightining)]
		[HDR]	// rim lightining
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
	}
	SubShader
	{
		Pass
		{
			Tags
			{
				// to interact with main directional light
				"LightMode" = "ForwardBase"	// request lighting data to be passed t shader
				"PassFlags" = "OnlyDirectional" // restrict the data recivied to be on directional light only
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
				// the normal in aappdata are populated automaticlly
				float3 normal : Normal; // we need accces to object normal data
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				// the normal in v2 will be populated in the vertex function
				float3 worldNormal : Normal;
				float3 viewDir : TEXCOORD1; //This is the direction from the current vertex towards the camera
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _AmbientColor;
			float _BlendingMaxValue;

			float _Glossiness;
			float4 _SpecularColor;

			float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;
			
			v2f vert (appdata v)
			{
				v2f o;
				//transform the normal from object space to world space
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				// specular reflection
				o.viewDir =  WorldSpaceViewDir(v.vertex);

				return o;
			}
			
			float4 _Color;

			float4 frag (v2f i) : SV_Target
			{
				float3 normal = normalize(i.worldNormal);
				float NdotL = dot(_WorldSpaceLightPos0, normal);
				// smooth the edges between dark and light area
				float lightIntensity = smoothstep(0, _BlendingMaxValue, NdotL);   // divide the ligtining into two bands light and dark
				float4 light = lightIntensity * _LightColor0; //to change according to global directional light

				float3 viewDir = normalize(i.viewDir);

				//Blinn-Phong specular reflection is done by dot roduct betweeen the normal of the surface and the half vector
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);

				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;
				
				//rim lightining
				float4 rimDot = 1 - dot(viewDir, normal);
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float4 rim = rimIntensity * _RimColor;

				float4 sample = tex2D(_MainTex, i.uv);

				return _Color * sample * (light + _AmbientColor + specular + rim);
			}
			ENDCG
		}
		// to make cast and recive shadows , use pass graps a pass from different shader and use it
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}