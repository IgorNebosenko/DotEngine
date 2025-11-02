namespace Kernel.Project;

public class ProjectInstance
{
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
        
        Console.WriteLine("Stub");
        return;
        //TODO write all of this!
        
        AssetsInstance = new AssetsInstance(projectDirectory);
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
}