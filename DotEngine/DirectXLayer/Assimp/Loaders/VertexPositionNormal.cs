using SharpDX;

namespace DirectXLayer.Assimp.Loaders;

public struct VertexPositionNormal
{
    public Vector3 position;
    public Vector3 normal;

    public VertexPositionNormal(Vector3 position, Vector3 normal)
    {
        this.position = position;
        this.normal = normal;
    }
}