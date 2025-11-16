using Attributes;
using DxStructures;

namespace DotEngine;

public class MeshFilter : Component
{
    public string Name => "MeshFilter";

    [ShowInInspector] private ShowInExplorerReference reference;
    
    private List<VertexModelData> _vertices;
    public IReadOnlyList<VertexModelData> Vertices => _vertices;
}