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

        layers = new []
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

    public bool Validate(out List<string> errorMessages)
    {
        errorMessages = new List<string>();
        var status = true;
        
        if (layers.Length != LayersCount)
        {
            errorMessages.Add($"Layers count must be equal to {LayersCount}");
            status = false;
        }
        
        return status;
    }
}