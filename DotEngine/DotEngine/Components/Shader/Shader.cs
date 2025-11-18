using DotEngine.Components;
using Newtonsoft.Json;
using SharpDX.D3DCompiler;

namespace DxStructures;

[Serializable]
public class Shader : IComponent
{
    private CompilationResult? _pixelShader;
    private CompilationResult? _vertexShader;
    
    private static Dictionary<string, CompilationResult?> _pixelShaders = new();
    private static Dictionary<string, CompilationResult?> _vertexShaders = new();

    public void LoadFromJson(string json)
    {
        var shaderLoadData = JsonConvert.DeserializeObject<ShaderLoadData>(json);

        if (shaderLoadData == null)
        {
            return;
        }
        
        if (_pixelShaders.TryGetValue(shaderLoadData.pixelShaderData, out CompilationResult? pixelShader))
        {
            _pixelShader = pixelShader;
        }
        else
        {
            _pixelShaders.Add(shaderLoadData.pixelShaderData, 
                ShaderBytecode.Compile(shaderLoadData.pixelShaderData, shaderLoadData.pixelEntry, shaderLoadData.pixelModel));
        }

        if (_vertexShaders.TryGetValue(shaderLoadData.vertexShaderData, out CompilationResult? vertexShader))
        {
            _vertexShader = vertexShader;
        }
        else
        {
            _vertexShaders.Add(shaderLoadData.vertexShaderData, 
                ShaderBytecode.Compile(shaderLoadData.vertexShaderData, shaderLoadData.vertexEntry, shaderLoadData.vertexModel));
        }
    }
}