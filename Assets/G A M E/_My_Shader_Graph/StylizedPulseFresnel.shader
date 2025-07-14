Shader "Custom/HolographicPulseGlow"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (0.1, 0.4, 1.0, 1)
        _EmissionColor ("Emission Color", Color) = (0.3, 1.0, 1.0, 1)
        _PulseSpeed ("Pulse Speed", Float) = 3.0
        _GlowStrength ("Glow Strength", Float) = 2.0
        _TwirlStrength ("Twirl Strength", Float) = 5.0
        _NoiseTex ("Noise Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _NoiseTex;

            float4 _MainColor;
            float4 _EmissionColor;
            float _PulseSpeed;
            float _GlowStrength;
            float _TwirlStrength;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(_WorldSpaceCameraPos - worldPos);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Twirl UV distortion
                float2 uv = i.uv - 0.5;
                float angle = atan2(uv.y, uv.x) + _Time.y * 0.5;
                float radius = length(uv);
                uv.x = cos(angle) * radius;
                uv.y = sin(angle) * radius;
                uv = uv * 1.5 + 0.5;

                float noise = tex2D(_NoiseTex, uv).r;

                float fresnel = pow(1.0 - saturate(dot(i.viewDir, i.worldNormal)), 3.0);
                float pulse = sin(_Time.y * _PulseSpeed) * 0.5 + 0.5;
                float glow = fresnel * pulse * _GlowStrength;

                float4 finalColor = _MainColor + _EmissionColor * glow * noise;

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
