namespace Kernel.Project;

public class BinInstance : IProjectInstance
{
    private string _folderPath;
    
    public BinInstance(string basePath)
    {
        _folderPath = basePath;
        
        Load();
    }
    
    public string FolderPath => "Bin";
    
    public void Load()
    {
        throw new NotImplementedException();
    }
}