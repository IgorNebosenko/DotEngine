namespace Kernel.Project;

public class ConfigsInstance : IProjectInstance
{
    private string _folderPath;
    
    public ConfigsInstance(string projectDirectory)
    {
        _folderPath = projectDirectory;
        Load();
    }
    
    public string FolderPath => "Configs";
    
    public void Load()
    {
        throw new NotImplementedException();
    }
}