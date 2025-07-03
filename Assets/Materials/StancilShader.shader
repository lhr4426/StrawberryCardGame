// URP 2D 전용 - Spherical Mask Dissolve Shader (Shader Graph 없이 HLSL)
Shader "URP2D/SphericalMaskDissolve"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _NoiseSize ("Noise Size", Float) = 1
        _Cutoff ("Cutoff", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _BaseColor;
            float4 _EmissionColor;
            float _NoiseSize;
            float _Cutoff;

            float3 _GLOBALMaskPosition;
            float _GLOBALMaskRadius;
            float _GLOBALMaskSoftness;

            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.worldPos = mul(unity_ObjectToWorld, IN.positionOS).xyz;
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                float dist = distance(IN.worldPos, _GLOBALMaskPosition);
                float mask = 1 - saturate((dist - _GLOBALMaskRadius) / _GLOBALMaskSoftness);

                float dissolveNoise = random(floor(IN.uv * _NoiseSize));
                clip(mask - _Cutoff);

                float edge = step(mask - _Cutoff, 0.1) * dissolveNoise;

                float4 tex = tex2D(_MainTex, IN.uv);
                float4 finalColor = tex * _BaseColor;
                finalColor.rgb += _EmissionColor.rgb * edge;
                return finalColor;
            }
            ENDHLSL
        }
    }
}
