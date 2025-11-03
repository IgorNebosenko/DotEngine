using Newtonsoft.Json;

namespace Kernel.Engine;

public class EngineMetaDataHolder
{
    private static readonly string MetaDataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DotEngine");
    private static readonly string EngineDataPath = Path.Combine(MetaDataPath, "EngineData.json");
    
    public EngineData? Data { get; private set; }
    
    public bool TryReadMetaData()
    {
        if (!Directory.Exists(MetaDataPath) || !File.Exists(EngineDataPath))
            return false;

        var json = File.ReadAllText(EngineDataPath);
        Data = JsonConvert.DeserializeObject<EngineData>(json);

        return true;
    }
}