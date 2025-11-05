using System.ComponentModel.DataAnnotations;
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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (fixedDeltaTimeStep < float.Epsilon)
            yield return new ValidationResult("Fixed DeltaTimeStep must be greater than 0");
            
        if (timeScale < float.Epsilon)
            yield return new ValidationResult("TimeScale must be greater than 0");
        
        yield break;
    }
}