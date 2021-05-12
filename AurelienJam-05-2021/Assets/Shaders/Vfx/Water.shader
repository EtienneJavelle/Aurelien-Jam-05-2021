// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "_AurelienGameJam/Water"
{
	Properties
	{
		_Texture0("Texture 0", 2D) = "white" {}
		_Speeddirection("Speed & direction", Vector) = (0.2,0.5,0,0)
		[HDR]_Water_Tint("Water_Tint", Color) = (0,0,0,0)
		_Edge_Power("Edge_Power", Float) = 0
		_Edge_Distace("Edge_Distace", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _Texture0;
		uniform float2 _Speeddirection;
		uniform float4 _Water_Tint;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Edge_Distace;
		uniform float _Edge_Power;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner3 = ( 1.0 * _Time.y * _Speeddirection + i.uv_texcoord);
			float4 tex2DNode10 = tex2D( _Texture0, panner3 );
			o.Albedo = ( tex2DNode10 * _Water_Tint ).rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth54 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth54 = abs( ( screenDepth54 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Edge_Distace ) );
			float3 temp_cast_1 = (saturate( ( ( 1.0 - distanceDepth54 ) * _Edge_Power ) )).xxx;
			o.Emission = temp_cast_1;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18712
340;296;1692;770;1341.28;567.7515;1.122969;True;False
Node;AmplifyShaderEditor.RangedFloatNode;53;-732.6226,36.81519;Inherit;False;Property;_Edge_Distace;Edge_Distace;10;0;Create;True;0;0;0;False;0;False;0;0.28;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;54;-545.5087,14.63837;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;5;-1378.701,-375.9994;Inherit;False;Property;_Speeddirection;Speed & direction;1;0;Create;True;0;0;0;False;0;False;0.2,0.5;0.015,0.015;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1368.701,-524.3193;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;1;-1119.701,-647.3192;Inherit;True;Property;_Texture0;Texture 0;0;0;Create;True;0;0;0;False;0;False;039f02ffcb3815348869af3fe999d3f9;24b6fda455027a248992912ebbf49251;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.OneMinusNode;55;-265.5296,29.88538;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-291.9854,168.0919;Inherit;False;Property;_Edge_Power;Edge_Power;9;0;Create;True;0;0;0;False;0;False;0;1.34;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;3;-1110.701,-436.3193;Inherit;False;3;0;FLOAT2;1,1;False;2;FLOAT2;0.6,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-64.55465,93.64285;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;51;-432.1232,-305.4348;Inherit;False;Property;_Water_Tint;Water_Tint;8;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-780.2206,-539.319;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldNormalVector;31;-3120.924,281.4124;Inherit;False;True;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PosVertexDataNode;25;-2415.401,210.8602;Inherit;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;58;118.4017,214.2277;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;35;-2023.767,768.0775;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;33;-2835.836,511.7861;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-1280.807,318.8481;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-999.1574,-52.85839;Inherit;False;Property;_Float1;Float 1;3;0;Create;True;0;0;0;False;0;False;0;1.79;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1120.118,-156.5384;Inherit;False;Property;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;0;-0.35;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;45;-989.7481,-871.1332;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleTimeNode;50;-1181.108,-803.3241;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-225.5057,-684.2324;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CameraDepthFade;7;-699.0615,-232.9595;Inherit;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;49;-625.8275,-863.5876;Inherit;True;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.ScaleAndOffsetNode;44;-53.32554,-733.0167;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-33.23782,-347.0451;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ObjectToWorldMatrixNode;9;-1016.74,-273.6388;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.PowerNode;34;-2503.234,598.1772;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CameraDepthFade;41;-1970.494,170.5449;Inherit;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-2889.109,798.3146;Inherit;False;Property;_Edging;Edging;5;0;Create;True;0;0;0;False;0;False;0;0.12;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;40;-1699.802,415.318;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-1093.16,276.2283;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;38;-2068.401,524.7461;Inherit;False;Constant;_Color0;Color 0;7;0;Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-1511.183,544.9031;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-1719.959,557.8623;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;32;-3135.322,523.3062;Inherit;False;World;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;30;-2408.203,389.4005;Inherit;False;Property;_softFactor;softFactor;4;0;Create;True;0;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;42;-2222.465,51.03802;Inherit;False;Property;_Float2;Float 2;6;0;Create;True;0;0;0;False;0;False;0;1.06;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-845.3565,-757.4094;Inherit;False;Property;_Scale;Scale;7;0;Create;True;0;0;0;False;0;False;0;-1.37;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;29;-2039.604,369.2429;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;322.4318,-110.9693;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;_AurelienGameJam/Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;54;0;53;0
WireConnection;55;0;54;0
WireConnection;3;0;4;0
WireConnection;3;2;5;0
WireConnection;57;0;55;0
WireConnection;57;1;56;0
WireConnection;10;0;1;0
WireConnection;10;1;3;0
WireConnection;58;0;57;0
WireConnection;35;0;34;0
WireConnection;33;0;31;0
WireConnection;33;1;32;0
WireConnection;28;0;41;0
WireConnection;28;1;39;0
WireConnection;48;0;49;0
WireConnection;48;1;10;0
WireConnection;7;2;9;0
WireConnection;7;0;11;0
WireConnection;7;1;12;0
WireConnection;49;0;45;0
WireConnection;49;1;50;0
WireConnection;49;2;47;0
WireConnection;44;2;48;0
WireConnection;52;0;10;0
WireConnection;52;1;51;0
WireConnection;34;0;33;0
WireConnection;34;1;36;0
WireConnection;41;2;25;0
WireConnection;41;0;42;0
WireConnection;40;0;29;0
WireConnection;43;1;28;0
WireConnection;39;0;37;0
WireConnection;39;1;40;0
WireConnection;37;0;38;0
WireConnection;37;1;35;0
WireConnection;29;1;25;0
WireConnection;29;0;30;0
WireConnection;0;0;52;0
WireConnection;0;2;58;0
ASEEND*/
//CHKSM=14C1094AC3411916F08A0724F869B5C72D0163C9