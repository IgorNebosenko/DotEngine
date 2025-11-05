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
}