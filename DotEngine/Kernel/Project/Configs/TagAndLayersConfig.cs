using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Kernel.Project.Configs;

[Serializable]
public class TagAndLayersConfig : IProjectConfig
{
    private const int LayersCount = 32;
    
    public string[] tags;
    public string[] layers;

    public TagAndLayersConfig()
    {
        tags = new []
        {
            "No tag",
            "Respawn",
            "Finish",
            "Player",
            "Enemy",
            "EditorOnly"
        };

        layers = new string[LayersCount]
        {
            "Default",
            "TransparentFX",
            "IgnoreRaycast",
            "Ground",
            "Water",
            "UI",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        };
    }
    
    [JsonIgnore]
    public string ConfigFile => "TagsAndLayersConfig.json";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (layers.Length != LayersCount)
            yield return new ValidationResult($"Count of layers must be {LayersCount}!");
        
        yield break;
    }
}