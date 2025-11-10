namespace Kernel.Project;

public class BinInstance : IProjectInstance
{
    private string _folderPath;
    
    public BinInstance(string basePath)
    {
        _folderPath = basePath;
    }
    
    public string FolderPath => "Bin";
    
    public void Load()
    {
        Console.WriteLine("Loading Bin isn't implemented yet");
    }
}