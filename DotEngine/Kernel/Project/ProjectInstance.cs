using Kernel.Project.Assets;
using Kernel.Project.Configs;

namespace Kernel.Project;

public class ProjectInstance
{
    private string _projectDirectory;
    
    public AssetsInstance AssetsInstance { get; private set; }
    public BinInstance BinInstance { get; private set; }
    public CacheInstance CacheInstance { get; private set; }
    public ConfigsInstance ConfigsInstance { get; private set; }
    public LogsInstance LogsInstance { get; private set; }
    public PackagesInstance PackagesInstance { get; private set; }
    public UserSettingsInstance UserSettingsInstance { get; private set; }
    
    private IProjectInstance[] _projectItems;

    public IReadOnlyList<IProjectInstance> Items => _projectItems;
    
    public ProjectInstance(string projectDirectory)
    {
        _projectDirectory = projectDirectory;
        
        AssetsInstance = new AssetsInstance(_projectDirectory);
        
        BinInstance = new BinInstance(projectDirectory);
        CacheInstance = new CacheInstance(projectDirectory);
        ConfigsInstance = new ConfigsInstance(projectDirectory);
        LogsInstance = new LogsInstance(projectDirectory);
        PackagesInstance = new PackagesInstance(projectDirectory);
        UserSettingsInstance = new UserSettingsInstance(projectDirectory);
        
        _projectItems =
        [
            AssetsInstance,
            BinInstance,
            CacheInstance,
            ConfigsInstance,
            LogsInstance,
            PackagesInstance,
            UserSettingsInstance
        ];
    }

    public void CheckAllDirectories()
    {
        for (var i = 0; i < _projectItems.Length; i++)
        {
            var fullPath = Path.Combine(_projectDirectory, _projectItems[i].FolderPath);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }
    }

    public void Load()
    {
        AssetsInstance.Load();
        ConfigsInstance.Load();
    }
}