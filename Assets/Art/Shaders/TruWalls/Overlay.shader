Shader "Unlit/Overlay" // Object behind the walls
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OverlayColor ("OverlayColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        // Apariencia base de la esfera
        Pass
        {
            Name "Opaque 1st pass"

            Tags {
                "RenderType"="Opaque"
                "Queue"= "Geometry"
                 }
             

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }

        Pass
        {
            Name "Overlay 2nd pass"

            Tags {
                "RenderType"="Opaque"
                "Queue"= "Geometry+1" // 2001 
                // Se procesa por el z-buffer DESPUÉS de la geometría
                
                }

            ZTest GEqual    
            // Dibuja los que están detrás o a la misma distancia. 
            // NO DIBUJA A LOS QUE ESTÁN DELANTE

            Stencil {
                Ref 1
                Comp Equal   // Si encuentra un 1, se dibuja. Si no, no.
                //Pass Replace // (Lo comento. No es fundamental para este ejemplo)
            }
            // Máscara: el overlay solo se muestra cuando hay muros
            // Si Ref coincide con lo que hay en el Stencil Buffer, reemplaza el valor del buffer y pon 1.


            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float4 _OverlayColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _OverlayColor;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
        
    }
}
