Shader "Custom/Mask" {
    Properties {
        _MainTex("Base (RGB)", 2D) = "white" {}

        _MaskColor ("MaskColor", Color) = (0,0,0,1)
        _MaskTexture ("MaskTexture", 2D) = "white" {}
        _VisibleRadius ("VisibleRadius", Float) = 0.0
        _VisibleAngle ("VisibleAngle", Float) = 0.0
        _VisibleDistance ("VisibleDistance", Float) = 0.0
        _GradientLength ("GradientLength", Float) = 0.0
        _GradientAngle ("GradientAngle", Float) = 0.0
        _MousePosition ("MousePosition", Vector) = (0.0, 0.0, 0.0, 0.0)
        _CameraPosition ("CameraPosition", Vector) = (0.0, 0.0, 0.0, 0.0)
        _ViewDirection ("ViewDirection", Vector) = (0.0, 0.0, 0.0, 0.0)
        _FocusedPosition ("FocusedPosition", Vector) = (0.0, 0.0, 0.0, 0.0)
        _ScreenWidth ("ScreenWidth", Float) = 0.0
        _ScreenHeight ("ScreenHeight", Float) = 0.0
    }

    SubShader {
        CGINCLUDE
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        half4 _MainTex_TexelSize;

        float4 _MaskColor;

        sampler2D _MaskTexture;
        half4 _MaskTexture_TexelSize;

        float _VisibleRadius;
        float _VisibleAngle;
        float _VisibleDistance;
        float _GradientLength;
        float _GradientAngle;

        float4 _MousePosition;
        float4 _CameraPosition;
        float4 _ViewDirection;
        float4 _FocusedPosition;

        float _ScreenWidth;
        float _ScreenHeight;

        struct v2f {
            float4 vertex : SV_POSITION;
            half2 uv : TEXCOORD0;
            half2 uv_depth : TEXCOORD1;
        };

        v2f vert(appdata_img v) {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.texcoord;
            o.uv_depth = v.texcoord;
            #if UNITY_UV_STARTS_AT_TOP
            if (_MaskTexture_TexelSize.y < 0) {
                o.uv_depth.y = 1 - o.uv_depth.y;
            }
            #endif
            return o;
        }

        fixed4 frag(v2f i) : SV_Target{
            float2 uv = i.uv;
            float4 texColor = tex2D(_MainTex, i.uv);

            float4 color = _MaskColor;
            float4 pos = i.vertex;
            pos.y = _ScreenHeight - pos.y;
            float4 focusPos = _FocusedPosition;
            float dis = length(pos.xy - focusPos.xy);
            float dis1 = clamp(dis, _VisibleRadius, _VisibleRadius + _GradientLength);
            float factor1 = smoothstep(_VisibleRadius, _VisibleRadius + _GradientLength, dis1);
            
            float2 viewDir = normalize(_ViewDirection.xy);
            float2 dir = normalize(pos.xy - focusPos.xy);
            float cosDir = dot(viewDir, dir);
            float vRadian = _VisibleAngle * 0.0087266462599716478846184538424;
            float gRadian = (_VisibleAngle + _GradientAngle) * 0.0087266462599716478846184538424;
            float cosViewAngle = cos(vRadian);
            float cosGradientAngle = cos(gRadian);
            float cosClamp = clamp(cosDir, cosGradientAngle, cosViewAngle);
            float factor2 = 1 - smoothstep(cosGradientAngle, cosViewAngle, cosClamp);

            float dis3 = clamp(dis, _VisibleDistance, _VisibleDistance + _GradientLength);
            float factor3 = smoothstep(_VisibleDistance, _VisibleDistance + _GradientLength, dis3);
            
            // return lerp(texColor, color, factor2);
            return lerp(texColor, color, factor1 * (1 - (1 - factor2) * (1 - factor3)));
        }
        ENDCG

        Pass {
            ZTest Always
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
    FallBack Off
}
