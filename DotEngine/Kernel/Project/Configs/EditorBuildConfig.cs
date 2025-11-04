using Kernel.Engine;
using Newtonsoft.Json;

namespace Kernel.Project.Configs;

[Serializable]
public class EditorBuildConfig : IProjectConfig
{
    public string[] scenesHashes;
    public Platform targetPlatform;

    public EditorBuildConfig()
    {
        targetPlatform = Platform.WindowsX64;
    }
    
    [JsonIgnore]
    public string ConfigFile => "EditorConfig.json";
}