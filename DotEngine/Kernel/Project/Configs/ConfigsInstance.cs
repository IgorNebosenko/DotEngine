namespace Kernel.Project.Configs;

public class ConfigsInstance : IProjectInstance
{
    private string _projectDirectory;
    
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
        _audioConfig = LoadOrCreateProjectConfig<AudioConfig>(Path.Combine(FullPath, "Audio.config"));
        _editorBuildConfig = LoadOrCreateProjectConfig<EditorBuildConfig>(Path.Combine(FullPath, "EditorBuild.config"));
        _projectConfig = LoadOrCreateProjectConfig<ProjectConfig>(Path.Combine(FullPath, "Project.config"));
    }

    private T LoadOrCreateProjectConfig<T>(string fullPath) where T : class, IProjectConfig
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}