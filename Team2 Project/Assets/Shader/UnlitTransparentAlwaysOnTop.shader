	 Shader "Custom/UnlitTransparentAlwaysOnTop"
	{
	    Properties
	    {
	        _MainTex ("Albedo (RGB)", 2D) = "white" {}
	        _Color ("Tint Color", Color) = (1, 1, 1, 1)
	        
	        _StencilComp ("Stencil Comparison", Float) = 8
	        _Stencil ("Stencil ID", Float) = 0
	        _StencilOp ("Stencil Operation", Float) = 0
	        _StencilWriteMask ("Stencil Write Mask", Float) = 255
	        _StencilReadMask ("Stencil Read Mask", Float) = 255
	    }
	    
	    SubShader
	    {
	        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
	        
	        Stencil
	        {
	            Ref [_Stencil]
	            Comp [_StencilComp]
	            Pass [_StencilOp]
	            ReadMask [_StencilReadMask]
	            WriteMask [_StencilWriteMask]
	        }
	 
	        ZWrite Off
	        ZTest Off
	        
	        Pass
	        {
	            Blend SrcAlpha OneMinusSrcAlpha
	            
	            CGPROGRAM
	            #pragma vertex vert
	            #pragma fragment frag
	            
	            #include "UnityCG.cginc"
	            
	            struct appdata
	            {
	                float4 vertex : POSITION;
	                float2 uv : TEXCOORD0;
	            };
	            
	            struct v2f
	            {
	                float2 uv : TEXCOORD0;
	                float4 vertex : SV_POSITION;
	            };
	            
	            sampler2D _MainTex;
	            fixed4 _Color;
	            
	            v2f vert (appdata v)
	            {
	                v2f o;
	                o.vertex = UnityObjectToClipPos(v.vertex);
	                o.uv = v.uv;
	                return o;
	            }
	            
	            fixed4 frag (v2f i) : SV_Target
	            {
	                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
	                col.a = _Color.a; // Preserve the original alpha value
	                return col;
	            }
	            ENDCG
	        }
	    }
	}