using Newtonsoft.Json;

namespace Kernel.Project.Configs;

public class ConfigsInstance : IProjectInstance
{
    private string _projectDirectory;
    
    private IReadOnlyList<IProjectConfig> _configs;
    
    private AudioConfig _audioConfig;
    private EditorBuildConfig _editorBuildConfig;
    private ProjectConfig _projectConfig;
    
    public ConfigsInstance(string projectDirectory)
    {
        _projectDirectory = projectDirectory;
    }
    
    public string FolderPath => "Configs";
    private string FullPath => Path.Combine(_projectDirectory, FolderPath);
    
    public void Load()
    {
        _audioConfig = LoadOrCreateProjectConfig<AudioConfig>();
        _editorBuildConfig = LoadOrCreateProjectConfig<EditorBuildConfig>();
        _projectConfig = LoadOrCreateProjectConfig<ProjectConfig>();
        
        _configs = new List<IProjectConfig>
        {
            _audioConfig,
            _editorBuildConfig,
            _projectConfig
        };
        
        Save();
    }

    private T LoadOrCreateProjectConfig<T>() where T : class, IProjectConfig, new()
    {
        var defaultInstance = new T();
        
        var fullPath = Path.Combine(FullPath, defaultInstance.ConfigFile);
        if (File.Exists(fullPath))
        {
            var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(fullPath));

            if (data == null)
            {
                return defaultInstance;
            }
            
            return data;
        }
        
        return defaultInstance;
    }

    public void Save()
    {
        for (var i = 0;  i < _configs.Count; i++)
        {
            File.WriteAllText(Path.Combine(FullPath, _configs[i].ConfigFile), 
                JsonConvert.SerializeObject(_configs[i]));
        }
    }
}