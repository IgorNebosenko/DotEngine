using Newtonsoft.Json;

namespace Kernel.Project.Configs;

public class ConfigsInstance : IProjectInstance
{
    private string _projectDirectory;
    
    private IReadOnlyList<IProjectConfig> _configs;
    
    private AudioConfig _audioConfig;
    private EditorBuildConfig _editorBuildConfig;
    private EditorConfig _editorConfig;
    private InputConfig _inputConfig;
    private PhysicsConfig _physicsConfig;
    private ProjectConfig _projectConfig;
    private QualityConfig _qualityConfig;
    private TagAndLayersConfig _tagAndLayersConfig;
    private TimeConfig _timeConfig;
    
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
        _inputConfig = LoadOrCreateProjectConfig<InputConfig>();
        _physicsConfig = LoadOrCreateProjectConfig<PhysicsConfig>();
        _projectConfig = LoadOrCreateProjectConfig<ProjectConfig>();
        _qualityConfig = LoadOrCreateProjectConfig<QualityConfig>();
        _tagAndLayersConfig  = LoadOrCreateProjectConfig<TagAndLayersConfig>();
        _timeConfig = LoadOrCreateProjectConfig<TimeConfig>();
        
        _configs = new List<IProjectConfig>
        {
            _audioConfig,
            _editorBuildConfig,
            _editorConfig,
            _inputConfig,
            _physicsConfig,
            _projectConfig,
            _qualityConfig,
            _tagAndLayersConfig,
            _timeConfig
        };
        
        Save();
    }

    private T LoadOrCreateProjectConfig<T>() where T : class, IProjectConfig, new()
    {
        var defaultInstance = new T();
        
        var fullPath = Path.Combine(FullPath, defaultInstance.ConfigFile);
        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"Not found {defaultInstance.ConfigFile}. Create new instance");
            
            return defaultInstance;
        }

        var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(fullPath));

        if (data == null)
        {
            Console.WriteLine($"Can't read or convert config file {defaultInstance.ConfigFile}!");

            return new T();
        }
        
        var validationStatus = data.Validate(out var errorStrings);

        if (!validationStatus)
        {
            for (var i = 0; i < errorStrings.Count; i++)
            {
                Console.WriteLine(errorStrings[i]);
            }

            return new T();
        }

        return data;
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