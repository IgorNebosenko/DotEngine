using System.ComponentModel.DataAnnotations;
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
    public string ConfigFile => "EditorBuildConfig.json";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (scenesHashes.GroupBy(x => x).Any(group => group.Count() > 1))
            yield return new ValidationResult("Some of scenes are equals in list!");
        
        yield break;
    }
}