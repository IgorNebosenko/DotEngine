using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Kernel.Project.Configs;

[Serializable]
public class EditorConfig : IProjectConfig
{
    public bool useShaderCompilation;
    public bool useAutoCompilation;
    
    public EditorConfig()
    {
        useAutoCompilation = true;
        useShaderCompilation = true;
    }
    
    [JsonIgnore]
    public string ConfigFile => "EditorConfig.json";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        yield break;
    }
}