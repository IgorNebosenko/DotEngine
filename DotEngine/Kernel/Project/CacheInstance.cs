namespace Kernel.Project;

public class CacheInstance : IProjectInstance
{
    private string _folderPath;
    
    public CacheInstance(string basePath)
    {
        _folderPath = basePath;
        
        Load();
    }
    
    public string FolderPath => "Cache";
    
    public void Load()
    {
        throw new NotImplementedException();
    }
}