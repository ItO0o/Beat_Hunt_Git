Shader "Custom/monoTone" {
    Properties {
        _MainTex("MainTex", 2D) = ""{}
        _MonoLevel("MonoLevel",  Range (0,1)) = 1
    }

    SubShader {
        Pass {
            CGPROGRAM

            #include "UnityCG.cginc"

            #pragma vertex vert_img
            #pragma fragment frag

            sampler2D _MainTex;
            float _MonoLevel;

            fixed4 frag(v2f_img i) : COLOR {
                fixed4 c = tex2D(_MainTex, i.uv);
                //float gray = c.r * 0.3 * _MonoLevel + c.g * 0.6 * _MonoLevel + c.b * 0.1 * _MonoLevel;
                //float gray = c.r * 0.3 + c.g * 0.6 + c.b * 0.1;
                float gray = c.r * 0.3 + c.g * 0.6 + c.b * 0.1;
                //gray = gray * _MonoLevel;
                c.rgb = fixed3(lerp(gray, c.r, _MonoLevel),lerp(gray, c.g, _MonoLevel),lerp(gray, c.b, _MonoLevel));
                return c;
            }

            ENDCG
        }
    }
}