using System.ComponentModel.DataAnnotations;
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

    public IProjectConfig DefaultInstance => new AudioConfig
    {
        volume = 1f,
        pitch = 1f
    };

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (volume < 0f || volume > 1f)
            yield return new ValidationResult("Incorrect volume! It must be in [0f...1f]");
        if (pitch < -10f || pitch > 10f)
            yield return new ValidationResult("Incorrect pitch!  It must be in [-10f...10f]");
        
        
    }
}