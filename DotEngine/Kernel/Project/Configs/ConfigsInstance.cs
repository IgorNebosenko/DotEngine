using Newtonsoft.Json;

namespace Kernel.Project.Configs;

public class ConfigsInstance : IProjectInstance
{
    private string _projectDirectory;
    
    private IReadOnlyList<IProjectConfig> _configs;
    
    private AudioConfig _audioConfig;
    private EditorBuildConfig _editorBuildConfig;
    private EditorConfig _editorConfig;
    private PhysicsConfig _physicsConfig;
    private ProjectConfig _projectConfig;
    private QualityConfig _qualityConfig;
    
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
        _editorConfig = LoadOrCreateProjectConfig<EditorConfig>();
        _physicsConfig = LoadOrCreateProjectConfig<PhysicsConfig>();
        _projectConfig = LoadOrCreateProjectConfig<ProjectConfig>();
        _qualityConfig = LoadOrCreateProjectConfig<QualityConfig>();
        Console.WriteLine("Fill with other configs and check json files!");
        
        _configs = new List<IProjectConfig>
        {
            _audioConfig,
            _editorBuildConfig,
            _editorConfig,
            _physicsConfig,
            _projectConfig,
            _qualityConfig
        };
        
        Save();
    }

    private T LoadOrCreateProjectConfig<T>() where T : class, IProjectConfig, new()
    {
        var defaultInstance = new T();
        
        var fullPath = Path.Combine(FullPath, defaultInstance.ConfigFile);
        if (!File.Exists(fullPath)) 
            return defaultInstance;
        var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(fullPath));

        return data ?? defaultInstance;
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