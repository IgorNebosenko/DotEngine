
using Newtonsoft.Json;

namespace Kernel.Project.Configs;

[Serializable]
public class AudioConfig : IProjectConfig
{
    public float volume;
    public float pitch;

    public AudioConfig()
    {
        volume = 1f;
        pitch = 1f;
    }
    
    [JsonIgnore]
    public string ConfigFile => "AudioConfig.json";
}