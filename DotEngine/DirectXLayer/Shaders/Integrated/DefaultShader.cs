namespace DirectXLayer.Shaders.Integrated;

public class DefaultShader : IShaderData
{
    public string Vertex => @"
                cbuffer ConstantBuffer : register(b0)
                {
                    matrix WorldViewProjection;
                };

                struct VS_INPUT
                {
                    float3 Pos : POSITION;
                    float4 Color : COLOR;
                };

                struct PS_INPUT
                {
                    float4 Pos : SV_POSITION;
                    float4 Color : COLOR;
                };

                PS_INPUT VS(VS_INPUT input)
                {
                    PS_INPUT output;
                    output.Pos = mul(float4(input.Pos, 1.0f), WorldViewProjection);
                    output.Color = input.Color;
                    return output;
                }";

    public string Pixel => @"
                struct PS_INPUT
                {
                    float4 Pos : SV_POSITION;
                    float4 Color : COLOR;
                };

                float4 PS(PS_INPUT input) : SV_Target
                {
                    return input.Color;
                }";
}