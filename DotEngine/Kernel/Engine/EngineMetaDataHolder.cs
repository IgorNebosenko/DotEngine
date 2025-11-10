using Newtonsoft.Json;

namespace Kernel.Engine;

public class EngineMetaDataHolder
{
    private static readonly string MetaDataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DotEngine");
    private static readonly string EngineDataPath = Path.Combine(MetaDataPath, "EngineData.json");
    
    public EngineData Data { get; private set; }
    
    public void HandleMetaData(Action<string> messageCallback, Func<string> createMetaDataCallback)
    {
        if (!Directory.Exists(MetaDataPath))
        {
            Directory.CreateDirectory(MetaDataPath);
        }

        if (!File.Exists(EngineDataPath))
        {
            File.Create(EngineDataPath).Dispose();
        }

        var json = File.ReadAllText(EngineDataPath);
        var dataTemp = JsonConvert.DeserializeObject<EngineData>(json);

        if (dataTemp == null || string.IsNullOrEmpty(dataTemp.LastProjectPath))
        {
            messageCallback?.Invoke("Can't find last opened project, please select correct folder, or create new one!");
            
            var projectPath = createMetaDataCallback?.Invoke();

            if (!string.IsNullOrEmpty(projectPath))
            {
                Data = new EngineData
                {
                    EngineVersion = EngineVersion.CurrentVersion,
                    LastProjectPath = projectPath,
                    IsDarkTheme = true
                };
                
                SaveMetaData();
            }
            else
            {
                messageCallback?.Invoke("Path to folder is empty! Initialization of engine is interrupted!");
            }
        }
        else
        {
            Data = dataTemp;
        }
    }

    public void SaveMetaData()
    {
        File.WriteAllText(EngineDataPath, JsonConvert.SerializeObject(Data));
    }
}