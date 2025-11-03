using DirectXLayer;
using Kernel.Engine;

namespace Kernel.Project.Configs;

[Serializable]
public class ProjectConfig : IProjectConfig
{
    public EngineVersion EngineVersion;

    public string ProjectName;
    public string CompanyName;
    public string ProjectVersion;
    public int BundleVersion;

    public DirectXVersion DirectXVersion;

    public string LastBinPath;

    public IProjectConfig DefaultInstance => new ProjectConfig
    {
        EngineVersion = EngineVersion.CurrentVersion,
        ProjectName = "DefaultInstance",
        CompanyName = "Default Company",
        ProjectVersion = "0.0.1",
        BundleVersion = 1,
        DirectXVersion = DirectXVersion.DirectX11
    };
}