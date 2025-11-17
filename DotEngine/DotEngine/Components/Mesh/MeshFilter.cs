using Attributes;
using DotEngine.Components;
using DxStructures;

namespace DotEngine;

public class MeshFilter : Component, IComponent
{
    private List<VertexModelData> _vertices;
    public IReadOnlyList<VertexModelData> Vertices => _vertices;

    public string Name => "MeshFilter";
    public ShowInExplorerReference Reference { get; private set; }
    public void Load()
    {
        throw new NotImplementedException();
    }
}