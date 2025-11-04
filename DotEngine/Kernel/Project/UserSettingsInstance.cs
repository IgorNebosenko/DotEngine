namespace Kernel.Project;

public class UserSettingsInstance : IProjectInstance
{
    private string _folderPath;
    
    public UserSettingsInstance(string folderPath)
    {
        _folderPath = folderPath;
    }
    
    public string FolderPath => "UserSettings";
    
    public void Load()
    {
        throw new NotImplementedException();
    }
}