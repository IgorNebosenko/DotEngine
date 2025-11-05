using Newtonsoft.Json;

namespace Kernel.Project.Configs;

[Serializable]
public class TimeConfig : IProjectConfig
{
    public float fixedDeltaTimeStep;
    public float timeScale;

    public TimeConfig()
    {
        fixedDeltaTimeStep = 0.02f;
        timeScale = 1f;
    }
    
    [JsonIgnore]
    public string ConfigFile => "TimeConfig.json";
}