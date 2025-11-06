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

    public bool Validate(out List<string> errorMessages)
    {
        errorMessages = new List<string>();
        var status = true;
        
        if (volume is < 0f or > 1f)
        {
            errorMessages.Add("Volume must be between 0f and 1f!");
            status = false;
        }

        if (pitch is < -10f or > 10f)
        {
            errorMessages.Add("Pitch must be between -10f and 10!");
            status = false;
        }

        return status;
    }
}