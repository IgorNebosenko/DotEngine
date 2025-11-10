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

    public bool Validate(out List<string> errorMessages)
    {
        errorMessages = new List<string>();
        var status = true;
        
        if (fixedDeltaTimeStep < float.Epsilon)
        {
            errorMessages.Add("Fixed delta time must be greater than 0!");
            status = false;
        }

        if (timeScale < 0 || timeScale > 100f)
        {
            errorMessages.Add("Time scale can't be negative or more 100!");
            status = false;
        }
        
        return status;
    }
}