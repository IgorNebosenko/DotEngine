namespace Kernel.Project;

public class LogsInstance : IProjectInstance
{
    private string _folderPath;
    
    public LogsInstance(string projectDirectory)
    {
        _folderPath = projectDirectory;
        Load();
    }
    
    public string FolderPath => "Logs";
    
    public void Load()
    {
        throw new NotImplementedException();
    }
}