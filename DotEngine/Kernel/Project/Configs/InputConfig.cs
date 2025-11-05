using System.ComponentModel.DataAnnotations;
using Kernel.Project.Configs.InputConfigs;
using Newtonsoft.Json;

namespace Kernel.Project.Configs;

[Serializable]
public class InputConfig : IProjectConfig
{
    public List<InputBinding> bindings;
    
    [JsonIgnore]
    public string ConfigFile => "InputConfig.json";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        yield break;
    }
}