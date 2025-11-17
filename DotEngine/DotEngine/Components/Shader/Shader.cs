using Attributes;
using DotEngine.Components;
using SharpDX.Direct3D11;

namespace DxStructures;

public class Shader : IComponent
{
    private VertexShader _vertexShader;
    private PixelShader _pixelShader;
    
    private static Dictionary<string, VertexShader> _vertexShaders = new();
    private static Dictionary<string, PixelShader> _pixelShaders = new();
    
    public ShowInExplorerReference Reference { get; private set; }

    public void Load()
    {
        
    }
}