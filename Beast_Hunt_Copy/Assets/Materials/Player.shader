Shader "Unlit/Player"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	   _Color("Color", Color) = (0,0,0,0)
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			Stencil
			{
				Ref 1
				Comp Always
				Pass Replace
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

				 c.rgb = _Color * shade * 2;

				return c;
			}
			ENDCG
		}
	}

		   Fallback "Diffuse"
}