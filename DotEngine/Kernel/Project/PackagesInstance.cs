namespace Kernel.Project;

public class PackagesInstance : IProjectInstance
{
    private string _folderPath;
    
    public PackagesInstance(string folderPath)
    {
        _folderPath = folderPath;
    }
    
    public string FolderPath => "Packages";
    
    public void Load()
    {
        throw new NotImplementedException();
    }
}