namespace Kernel.Project;

public class AssetsInstance : IProjectInstance
{
    private string _folderPath;
    
    public AssetsInstance(string basePath)
    {
        _folderPath = basePath;
        Load();
    }
    
    public string FolderPath => "Assets";
    
    public void Load()
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}