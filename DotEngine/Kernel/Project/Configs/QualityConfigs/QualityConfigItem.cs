namespace Kernel.Project.Configs.QualityConfigs;

[Serializable]
public class QualityConfigItem
{
    public string presetName;

    public float resolutionScalingFactor;
    public VSyncCount vSyncCount;

    public MipMapLimit mipMapLimit;
    public AnisotropicFiltering anisotropicFiltering;

    public int particleRaycastBudget;

    public QualityConfigItem()
    {
        presetName = "Default";
        
        resolutionScalingFactor = 1f;
        vSyncCount = VSyncCount.EveryVBank;

        mipMapLimit = MipMapLimit.FullResolution;
        anisotropicFiltering = AnisotropicFiltering.None;

        particleRaycastBudget = 4096;
    }
}