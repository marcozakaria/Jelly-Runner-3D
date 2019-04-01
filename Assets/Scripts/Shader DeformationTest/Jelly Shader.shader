Shader "Marco/Jelly Wiggle Shader" 
{
	Properties 
	{
		[Header(Main Texture)]
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo Base (RGB)", 2D) = "white" {}		
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		[Header(Wiggle Settings)]
		_WiggleTex("Wiggle Noise (RGB)", 2D) = "white" {}
		_WiggleStrength("Wiggle Strength", Range(0.01, 0.5)) = 0.01
		[Space(10)]
		_ControlTime("Time", float) = 0 //set by script

		[Header(vertex Deformation Settings)]
		_Frequency("Frequency", Range(0, 1000)) = 10
		_Amplitude("Amplitude", Range(0, 5)) = 0.2
		_WaveFalloff("Wave Falloff", Range(1, 8)) = 4
		_MaxWaveDistortion("Max Wave Distortion", Range(0.1, 2.0)) = 1
		_ImpactSpeed("Impact Speed", Range(0, 10)) = 0.5
		_WaveSpeed("Wave Speed", Range(-10, 10)) = -5

		[Header(Script Position Settings Dont change)]
		_ModelOrigin("Model Origin", Vector) = (0,0,0,0)
		_ImpactOrigin("Impact Origin", Vector) = (-5,0,0,0)

		[Header(Bending Settings)]
		[Toggle(IsBending)]  _AllowBending("Is Bending", Float) = 0 // not working
		_Curvature("Curvature Strength",Range(0.0005,0.01)) = 0.002
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// addshadow needs to be added in whenever you plan to modify vertex positions so that Unity makes a shadow caster pass on the object. Otherwise the shadows on your model will be generated based on the model’s original shape.
		#pragma surface surf Standard fullforwardshadows addshadow vertex:vert

		#pragma shader_feature IsBending // for tooggle

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _WiggleTex; // for destortion effect

		struct Input 
		{
			float2 uv_MainTex;
			float2 uv_WiggleTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _ControlTime;
		float _WiggleStrength; // for destortion effect water

		float4 _ModelOrigin;
		float4 _ImpactOrigin;

		half _Frequency; //Base frequency for our waves.
		half _Amplitude; //Base amplitude for our waves.

		half _WaveFalloff; //How quickly our distortion should fall off given distance.
		static half _MinWaveSize = 1;
		half _MaxWaveDistortion; //Smaller number here will lead to larger distortion as the vertex approaches origin.
		half _ImpactSpeed; //How quickly our wave origin moves across the sphere.
		half _WaveSpeed; //Oscillation "movement back and forth in a regular rhythm" speed of an individual wave.
		float _Curvature; // bending strenght
		float _AllowBending;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void vert(inout appdata_base v) 
		{
			// takes the vertex local position and multiplies it by unity provided matrix which brings vertex into world space
			float4 world_space_vertex = mul(unity_ObjectToWorld, v.vertex);
			// the direction wave will travel along the model,moves from the impact location towards the center of the mode
			float4 direction = normalize(_ModelOrigin - _ImpactOrigin);
			// variable to track the origin of our wave
			float4 origin = _ImpactOrigin + _ControlTime * _ImpactSpeed * direction;					

			//Get the distance in world space from our vertex to the wave origin.
			float dist = distance(world_space_vertex, origin);

			//Adjust our distance to be non-linear.
			dist = pow(dist, _WaveFalloff);

			//Set the max amount a wave can be distorted based on distance.
			dist = max(dist, _MaxWaveDistortion);

			//Convert direction and _ImpactOrigin to model space for later trig magic.
			float4 l_ImpactOrigin = mul(unity_WorldToObject, _ImpactOrigin);
			float4 l_direction = mul(unity_WorldToObject, direction);

			float impactAxis = l_ImpactOrigin + dot((v.vertex - l_ImpactOrigin), l_direction);
			v.vertex.xyz += v.normal * sin(impactAxis * _Frequency + _ControlTime * _WaveSpeed) * _Amplitude * (1/dist);

			 //bending shader section
#ifdef IsBending
			float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
			worldSpace.xyz -= _WorldSpaceCameraPos.xyz;
			worldSpace = float4(0.0f, ((worldSpace.z * worldSpace.z) + (worldSpace.x*worldSpace.x)) * -_Curvature, 0.0f, 0.0f);

			v.vertex += mul(unity_WorldToObject, worldSpace);
			 // end bending section
#endif
		}

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			// make the surf wiggle effect on the ball
			float2 tc2 = IN.uv_WiggleTex;
			tc2.x -= _SinTime;
			tc2.y += _CosTime;
			float4 wiggle = tex2D(_WiggleTex, tc2);

			// apply wiggle effect to texture
			IN.uv_MainTex.x -= wiggle.r * _WiggleStrength;
			IN.uv_MainTex.y += wiggle.b * _WiggleStrength*1.5;

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			// apply texture settings
			o.Albedo = c.rgb;
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
