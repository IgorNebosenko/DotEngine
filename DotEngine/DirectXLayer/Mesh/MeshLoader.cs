using System.IO;
using DirectXLayer.Assimp;
using DirectXLayer.Assimp.Loaders;
using DxStructures;
using SharpDX.Direct3D11;

namespace DirectXLayer.Mesh;

public class MeshLoader
{
    private readonly ILoader[] _loaders;

    public MeshLoader(Device device)
    {
        _loaders = new ILoader[]
        {
            new FbxLoader(device),
            new ObjLoader(device),
            new DaeLoader(device),
            new BlendLoader(device)
        };
    }

    public IReadOnlyList<VertexModelData> ReadModel(string path)
    {
        var emptyResult = new List<VertexModelData>();
        
        if (!Path.HasExtension(path))
        {
            Console.WriteLine($"Path {path} must have an extension!");
            return emptyResult;
        }
        
        var extension = Path.GetExtension(path);
        
        for (var i = 0; i < _loaders.Length; i++)
        {
            if (extension == _loaders[i].Extension)
            {
                return _loaders[i].ReadModel(path);
            }
        }

        return emptyResult;
    }
}