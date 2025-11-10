namespace Kernel.Project;

public class LogsInstance : IProjectInstance
{
    private string _folderPath;
    
    public LogsInstance(string projectDirectory)
    {
        _folderPath = projectDirectory;
    }
    
    public string FolderPath => "Logs";
    
    public void Load()
    {
        Console.WriteLine("Loading Logs isn't implemented yet");
    }
}