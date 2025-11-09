namespace DirectXLayer.Shaders.Integrated
{
    public class LitShader : IShaderData
    {
        public string Vertex => @"
cbuffer ConstantBuffer : register(b0)
{
    matrix WorldViewProjection;
}

struct VS_INPUT
{
    float3 pos : POSITION;
    float3 normal : NORMAL;
};

struct PS_INPUT
{
    float4 pos : SV_POSITION;
    float3 normal : NORMAL;
};

PS_INPUT VS(VS_INPUT input)
{
    PS_INPUT output;
    output.pos = mul(float4(input.pos, 1.0f), WorldViewProjection);
    output.normal = input.normal;
    return output;
}
";

        public string Pixel => @"
struct PS_INPUT
{
    float4 pos : SV_POSITION;
    float3 normal : NORMAL;
};

float4 PS(PS_INPUT input) : SV_TARGET
{
    float3 lightDir = normalize(float3(0.4, 0.7, -0.6));
    float diff = max(dot(normalize(input.normal), lightDir), 0.0);
    return float4(diff.xxx + 0.2, 1.0);
}
";
    }
}