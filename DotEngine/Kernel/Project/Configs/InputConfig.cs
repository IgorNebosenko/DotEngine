using Kernel.Project.Configs.InputConfigs;
using Newtonsoft.Json;

namespace Kernel.Project.Configs;

[Serializable]
public class InputConfig : IProjectConfig
{
    public List<InputBinding> bindings;
    
    [JsonIgnore]
    public string ConfigFile => "InputConfig.json";

    public bool Validate(out List<string> errorMessages)
    {
        errorMessages = new List<string>();
        return true;
    }
}