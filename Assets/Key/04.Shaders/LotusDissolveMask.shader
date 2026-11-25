Shader "Hidden/Key/LotusDissolveMask" {
    Properties {
        _MainTex ("Sprite", 2D) = "white" {}
        _SampleRect ("Sample Rect", Vector) = (0, 0, 1, 1)
        _NoiseScale ("Noise Scale", Float) = 80
        _NoiseStrength ("Noise Strength", Float) = 0.15
    }
    SubShader {
        Cull Off ZWrite Off ZTest Always
        Pass {
            HLSLPROGRAM
            #pragma target 3.5
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Hashes.hlsl"
            sampler2D _MainTex;
            float4 _SampleRect;
            float _NoiseScale;
            float _NoiseStrength;

            float2 NoiseDirection(float2 p) {
                float value;
                Hash_Tchou_2_1_float(p, value);
                return normalize(float2(value - floor(value + 0.5), abs(value) - 0.5));
            }

            float4 frag(v2f_img input) : SV_Target {
                float2 uv = _SampleRect.xy + input.uv * _SampleRect.zw;
                float2 p = uv * _NoiseScale;
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(NoiseDirection(ip), fp);
                float d01 = dot(NoiseDirection(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(NoiseDirection(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(NoiseDirection(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                float noise = lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x) + 0.5;
                return float4(uv.y + (noise - 0.5) * _NoiseStrength, 0, 0, tex2D(_MainTex, uv).a);
            }
            ENDHLSL
        }
    }
}
