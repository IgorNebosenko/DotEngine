using Attributes;
using DxStructures;

namespace DotEngine;

public class MeshRenderer : Component
{
    [ShowInInspector] private Shader[] meshShaders;
    [ShowInInspector] private bool castShadows;
    
    public string Name => "MeshRenderer";
}