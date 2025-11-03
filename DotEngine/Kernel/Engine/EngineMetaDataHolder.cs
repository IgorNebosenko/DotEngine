using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Kernel.Engine;

public class EngineMetaDataHolder
{
    private static readonly string MetaDataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DotEngine");
    private static readonly string EngineDataPath = Path.Combine(MetaDataPath, "EngineData.json");
    
    public EngineData Data { get; private set; }
    
    public void HandleMetaData()
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

        if (dataTemp == null)
        {
            MessageBox.Show("Can't find last opened project, please select correct folder, or create new one!");
            CreateMetaData();
            SaveMetaData();
        }
        else
        {
            Data = dataTemp;
        }
    }

    private void CreateMetaData()
    {
        var dialog = new OpenFolderDialog();

        void OnFolderOk(object? o, EventArgs eventArgs) => Data = new EngineData
        {
            EngineVersion = EngineVersion.CurrentVersion,
            LastProjectPath = dialog.FolderName,
            IsDarkTheme = true
        };

        dialog.FolderOk += OnFolderOk;
        dialog.ShowDialog();
        dialog.FolderOk -= OnFolderOk;
    }

    public void SaveMetaData()
    {
        File.WriteAllText(EngineDataPath, JsonConvert.SerializeObject(Data));
    }
}