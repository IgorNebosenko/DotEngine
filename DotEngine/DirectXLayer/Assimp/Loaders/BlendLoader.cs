using System.IO;
using Assimp;
using SharpDX;
using SharpDX.Direct3D11;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace DirectXLayer.Assimp.Loaders;

public class BlendLoader : ILoader
{
    private readonly Device _device;

    public BlendLoader(Device device)
    {
        _device = device;
    }

    public string Extension => ".blend";

    public IReadOnlyList<VertexModelData> ReadModel(string path)
    {
        var listResult = new List<VertexModelData>();
        
        if (!File.Exists(path))
        {
            Console.WriteLine($"File {path} does not exist");
            return listResult;
        }

        var importer = new AssimpContext();
        var scene = importer.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.GenerateNormals);
        
        for (var i = 0; i < scene.Meshes.Count; i++)
        {
            var vertices = scene.Meshes[i].Vertices.Select(v => new Vector3(v.X, v.Y, v.Z)).ToArray();
            var normals = scene.Meshes[i].Normals.Select(n => new Vector3(n.X, n.Y, n.Z)).ToArray();
            var indices = scene.Meshes[i].GetIndices();
            
            var vertexData = new VertexPositionNormal[vertices.Length];

            for (var j = 0; j < vertices.Length; j++)
            {
                vertexData[j] = new VertexPositionNormal(vertices[j], normals[j]);
            }
            
            var vertexBuffer = Buffer.Create(_device, BindFlags.VertexBuffer, vertexData);
            var indexBuffer = Buffer.Create(_device, BindFlags.IndexBuffer, indices);
            
            listResult.Add(new VertexModelData(vertexBuffer, indexBuffer, indices.Length));
        }

        return listResult;
    }
}