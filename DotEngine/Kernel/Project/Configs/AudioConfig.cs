namespace Kernel.Project.Configs;

[Serializable]
public class AudioConfig : IProjectConfig
{
    public float volume;
    public float pitch;
    
    public IProjectConfig DefaultInstance => new AudioConfig
    {
        volume = 1f,
        pitch = 1f
    };
}