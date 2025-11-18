namespace DxStructures;

[Serializable]
public class ShaderLoadData
{
    public string pixelShaderData;
    public string pixelEntry = "PS";
    public string pixelModel = "ps_4_0";
    
    public string vertexShaderData;
    public string vertexEntry = "VS";
    public string vertexModel = "vs_4_0";
}