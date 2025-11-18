using DotEngine.Components;
using DxStructures;
using Newtonsoft.Json;

namespace DotEngine;

public class MeshFilter : Component, IComponent
{
    private List<VertexModelData>? _vertices;
    public IReadOnlyList<VertexModelData> Vertices => _vertices;

    public string Name => "MeshFilter";

    public void LoadFromJson(string json)
    {
        _vertices = JsonConvert.DeserializeObject<List<VertexModelData>>(json);
    }
}