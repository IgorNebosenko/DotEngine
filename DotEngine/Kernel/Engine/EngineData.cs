namespace Kernel.Engine;

[Serializable]
public class EngineVersion(int TopVersion, int MiddleVersion, int LowVersion)
{
    public int TopVersion;
    public int MiddleVersion;
    public int LowVersion;

    public static EngineVersion CurrentVersion => new EngineVersion(0, 0, 1);
}

[Serializable]
public class EngineData
{
    public string LastProjectPath;
    public EngineVersion EngineVersion;
}