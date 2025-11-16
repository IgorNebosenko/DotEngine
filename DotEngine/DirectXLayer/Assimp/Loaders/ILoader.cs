using DxStructures;

namespace DirectXLayer.Assimp.Loaders;

public interface ILoader
{
    string Extension { get; }
    IReadOnlyList<VertexModelData> ReadModel(string path);
}