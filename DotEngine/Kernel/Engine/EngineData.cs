namespace Kernel.Engine;

[Serializable]
public class EngineVersion
{
    public static readonly EngineVersion CurrentVersion = new (0, 0, 1);
    
    public int TopVersion;
    public int MiddleVersion;
    public int LowVersion;

    public EngineVersion(int topVersion, int middleVersion, int lowVersion)
    {
        TopVersion = topVersion;
        MiddleVersion = middleVersion;
        LowVersion = lowVersion;
    }
}

[Serializable]
public class EngineData
{
    public string LastProjectPath;
    public EngineVersion EngineVersion;
    public bool IsDarkTheme;
}