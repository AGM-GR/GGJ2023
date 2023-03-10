Shader "Study/Outline" {

    Properties {

        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0

        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.03

    }

    Subshader {

        Tags {
            "RenderType" = "Opaque"
        }


        // 1st pass: Surface shader (renders the actual object)
        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        half4 _Color;
        half _Glossiness;
        half _Metallic;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {

            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Smoothness = _Glossiness;
            o.Metallic = _Metallic;
            o.Alpha = c.a;
        }

        ENDCG


        // Additional pass (OUTLINE)
        Pass {

            Cull Front // Solo renderiza la parte trasera de la mesh (la que no ve la c?mara)

            CGPROGRAM

            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram

            half _OutlineWidth;
            half4 _OutlineColor;

            float4 VertexProgram ( float4 position : POSITION, float3 normal : NORMAL) : SV_POSITION
            {
                position.xyz += normal * _OutlineWidth; // a?ade offset
                return UnityObjectToClipPos(position);
            }


            half4 FragmentProgram() : SV_TARGET 
            {
                return _OutlineColor; // Tinta la mesh con el nuevo pase de un color plano
            }

            ENDCG

        }

    }

}