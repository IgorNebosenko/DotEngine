using Newtonsoft.Json;
using SharpDX;

namespace Kernel.Project.Configs;

[Serializable]
public class PhysicsConfig : IProjectConfig
{
    public Vector3 gravity;

    public PhysicsConfig()
    {
        gravity = new Vector3(0f, -9.81f, 0f);
    }
    
    [JsonIgnore]
    public string ConfigFile => "PhysicsConfig.json";

    public bool Validate(out List<string> errorMessages)
    {
        errorMessages = new List<string>();
        return true;
    }
}