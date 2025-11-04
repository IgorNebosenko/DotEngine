using DirectXLayer;
using Kernel.Engine;
using Newtonsoft.Json;

namespace Kernel.Project.Configs;

[Serializable]
public class ProjectConfig : IProjectConfig
{
    public EngineVersion engineVersion;

    public string projectName;
    public string companyName;
    public string projectVersion;
    public int bundleVersion;

    public DirectXVersion directXVersion;

    public string LastBinPath;

    public ProjectConfig()
    {
        engineVersion = EngineVersion.CurrentVersion;
        projectName = "DefaultInstance";
        companyName = "Default Company";
        projectVersion = "0.0.1";
        bundleVersion = 1;
        directXVersion = DirectXVersion.DirectX11;
    }

    [JsonIgnore]
    public string ConfigFile => "ProjectConfig.json";
}