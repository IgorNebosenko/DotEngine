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
        scenesHashes = null;
        targetPlatform = Platform.WindowsX64;
    }
    
    [JsonIgnore]
    public string ConfigFile => "EditorBuildConfig.json";

    public bool Validate(out List<string> errorMessages)
    {
        errorMessages = new List<string>();
        var status = true;
        
        if (scenesHashes != null && scenesHashes.GroupBy(x => x).Any(x => x.Count() > 1))
        {
            errorMessages.Add("Found duplicate scenes!");
            status = false;
        }

        return status;
    }
}