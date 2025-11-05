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
    public string ConfigFile => "TagsAndLayers.json";
}