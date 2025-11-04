using Kernel.Engine;

namespace Kernel.Project.Configs;

[Serializable]
public class EditorBuildConfig : IProjectConfig
{
    public string[] scenesHashes;
    public Platform targetPlatform;
    
    public IProjectConfig DefaultInstance => new EditorBuildConfig
    {
        targetPlatform = Platform.WindowsX64
    };
}