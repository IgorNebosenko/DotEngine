namespace Kernel.Project.Configs;

public class ConfigsInstance : IProjectInstance
{
    private string _projectDirectory;
    private ProjectConfig _projectConfig;
    
    public ConfigsInstance(string projectDirectory)
    {
        _projectDirectory = projectDirectory;
    }
    
    public string FolderPath => "Configs";
    private string FullPath => Path.Combine(_projectDirectory, FolderPath);
    
    public void Load()
    {
        _projectConfig = (ProjectConfig)LoadOrCreateProjectConfig(Path.Combine(FullPath, "ProjectConfig.json"));
    }

    private IProjectConfig LoadOrCreateProjectConfig(string fullPath)
    {
        throw new NotImplementedException();
    }
}