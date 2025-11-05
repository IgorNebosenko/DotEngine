using System.ComponentModel.DataAnnotations;
using Kernel.Project.Configs.QualityConfigs;
using Newtonsoft.Json;

namespace Kernel.Project.Configs;

[Serializable]
public class QualityConfig : IProjectConfig
{
    public int currentQualityLevel;
    public int buildQualityLevel;

    public List<QualityConfigItem> qualityConfigItems;

    public QualityConfig()
    {
        currentQualityLevel = 0;
        buildQualityLevel = 0;
        
        qualityConfigItems = new ()
        {
            new QualityConfigItem()
        };
    }
    
    [JsonIgnore]
    public string ConfigFile => "QualityConfig.json";

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (currentQualityLevel < 0 || currentQualityLevel >= qualityConfigItems.Count)
            yield return new ValidationResult("Current quality level is out of range!");
        
        if (buildQualityLevel < 0 || buildQualityLevel >= qualityConfigItems.Count)
            yield return new ValidationResult("Current build quality level is out of range!");
    }
}