Shader "PickupShader"
{
	Properties
	{
		_Tint("Tint", Color) = (.5,.5,.5,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_OutlineThickness("Outline Thickness", Range(0, 0.05)) = .001
		_Texture("Texture", 2D) = "white" { }
	}

	CGINCLUDE
		#include "UnityCG.cginc"

		// When this keyword is active, this shader will work for cameras with orthographic projection
		#pragma shader_feature ORTHOGRAPHIC_IMPLEMENTATION

		struct appdata
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		};
		
		struct v2f
		{
			float4 pos : SV_POSITION;
			float4 color : COLOR;
		};

		uniform float _OutlineThickness;
		uniform float4 _OutlineColor;

		v2f vert(appdata v)
		{
			v2f o;
			
			
#if ORTHOGRAPHIC_IMPLEMENTATION
			// Start with the vertex position
			o.pos = v.vertex;

			// Offset the vertex position by the outline thickness
			o.pos.xyz += v.normal.xyz *_OutlineThickness;

			o.pos = mul(UNITY_MATRIX_MVP, o.pos);
#else
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
			float2 offset = TransformViewToProjection(norm.xy);
			o.pos.xy += offset * o.pos.z * _OutlineThickness;
#endif
			
			o.color = _OutlineColor;
			return o;
		}
	ENDCG

	SubShader
	{
		// Simple diffuse surface shader using the texture and tint
		CGPROGRAM
			#pragma surface surf Lambert

			sampler2D _Texture;
			fixed4 _Tint;

			struct Input
			{
				float2 uv_Texture;
			};

			void surf(Input IN, inout SurfaceOutput o)
			{
				fixed4 c = tex2D(_Texture, IN.uv_Texture) * _Tint;
				o.Albedo = c.rgb;
				o.Alpha = c.a;
			}
		ENDCG
		
		// Pass for solid color outline
		Pass
		{
			Name "OUTLINE"

			Tags { "LightMode" = "Always" }

			// Do not render the fronts of polygons in the outline pass or the whole object will be covered in the outline color
			Cull Front

			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
				// Use the vertex program defined in CGINCLUDE
				#pragma vertex vert
				
				// Only use the solid color for the outline
				#pragma fragment frag
				half4 frag(v2f i) : COLOR
				{
					return i.color;
				}
			ENDCG
		}
	}
	CustomEditor "ShaderInspector"
	Fallback "Diffuse"
}