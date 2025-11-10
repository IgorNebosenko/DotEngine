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

    public bool Validate(out List<string> errorMessages)
    {
        errorMessages = new List<string>();
        var status = true;
        
        if (qualityConfigItems == null || qualityConfigItems?.Count == 0)
        {
            errorMessages.Add("Quality levels must have at least one item!");
            status = false;
        }

        if (currentQualityLevel < 0 || currentQualityLevel >= qualityConfigItems?.Count)
        {
            errorMessages.Add("Current quality level is out of range!");
            status = false;
        }

        if (buildQualityLevel < 0 || buildQualityLevel >= qualityConfigItems?.Count)
        {
            errorMessages.Add("Build quality level is out of range!");
            status = false;
        }

        return status;
    }
}