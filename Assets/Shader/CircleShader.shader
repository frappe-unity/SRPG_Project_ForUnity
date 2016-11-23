Shader "Custom/CircleShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white"
	}
		SubShader
	{
		Tags {"Queue" = "Geometry+1" }
		ZTest Always
		ZWrite On

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// #include "UnityCG.cginc"

			uniform sampler2D _MainTex;

			struct v2f {
				float4 texPos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};
		
			v2f vert(float4 pos : POSITION, float2 uv : TEXCOORD0) {
				v2f OUT;
				OUT.texPos = mul(UNITY_MATRIX_MVP, pos);
				OUT.uv = uv;
				return OUT;
			}

			float4 frag(v2f input) : COLOR{
				return tex2D(_MainTex, input.uv);
			}

			ENDCG
		}
	}
}
