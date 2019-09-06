// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Tree"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
			_Color("Color", Color) = (1,1,1,1)
					_Parting("Shade Parting", Range(0,1)) = 0.5
	}
	  SubShader
			{
												Tags { "RenderType" = "Transparent"
						"Queue" = "Transparent"
						"IgnoreProjector" = "True"
			"LightMode" = "ForwardBase"}
				LOD 100
				Blend SrcAlpha OneMinusSrcAlpha

				Pass
				{
					Stencil
					{
						Ref 1
						Comp Equal
					}

					CGPROGRAM
					#pragma vertex vert_img
					#pragma fragment frag
					#include "UnityCG.cginc"

					sampler2D _MainTex;

					fixed4 frag(v2f_img i) : SV_Target
					{
						float alpha = tex2D(_MainTex, i.uv).a;
						fixed4 col = fixed4(0,0,0,alpha);
						return col;
					}
					ENDCG
				}

		Pass{
					Stencil
					{
						Ref 0
						Comp Equal
					}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			fixed4 _Color;
			sampler2D _MainTex;
			half _Parting;
			uniform fixed4 _LightColor0;

			struct v2f {
				float4 pos      : SV_POSITION;
				float3 lightDir : TEXCOORD0;
				float3 normal   : TEXCOORD1;
				float2 uv:TEXCOORD2;
				LIGHTING_COORDS(3, 4)
			};

			v2f vert(appdata_base v, float2 uv : TEXCOORD2) {
				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);
				o.lightDir = normalize(ObjSpaceLightDir(v.vertex));
				o.normal = normalize(v.normal).xyz;
				o.uv = uv;

				TRANSFER_VERTEX_TO_FRAGMENT(o);
				TRANSFER_SHADOW(o);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target{

				fixed atten = LIGHT_ATTENUATION(i);
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT;
				fixed4 lightColor = _LightColor0 * saturate(dot(i.lightDir, i.normal));

				fixed4 c = tex2D(_MainTex, i.uv) * _Color;

				fixed3 shade = (0.5 * lightColor * atten) + ambient;
				//fixed f = (shade.r < _Parting) ? -0.08 : 0;
				//fixed f = shade.r;
				c.rgb = c * shade * 2;

				return c;
			}
			ENDCG
		}
			}

				Fallback "Diffuse"
	

}