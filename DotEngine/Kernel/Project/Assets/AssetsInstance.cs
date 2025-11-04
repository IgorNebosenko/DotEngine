using Newtonsoft.Json;

namespace Kernel.Project.Assets;

public class AssetsInstance : IProjectInstance
{
    private const string DJsonExtension = ".djson";
    
    private string _folderPath;
    private Dictionary<string, AssetIdentifier> _assets;
    
    public AssetsInstance(string basePath)
    {
        _folderPath = basePath;
        _assets = new Dictionary<string, AssetIdentifier>();
    }
    
    private string FullPath => Path.Combine(_folderPath, "Assets");
    public string FolderPath => "Assets";
    
    public void Load()
    {
        _assets.Clear();

        var assetFiles = new List<string>(GetAllDirectoriesList());
        assetFiles.AddRange(Directory.GetFiles(FullPath, "*", SearchOption.AllDirectories));
        
        CleanUpDJson(assetFiles);
        
        var dJsonFiles = new List<string>(Directory.GetFiles(FullPath, $"*{DJsonExtension}", SearchOption.AllDirectories));
        DJsonCleanUp(dJsonFiles);
        DJsonCreate(assetFiles, dJsonFiles);
        AssetsLinks(assetFiles, dJsonFiles);
    }

    private IReadOnlyList<string> GetAllDirectoriesList()
    {
        var directories = new List<string>();

        if (string.IsNullOrWhiteSpace(FullPath) || !Directory.Exists(FullPath))
            return directories;

        var stack = new Stack<string>();
        stack.Push(FullPath);

        while (stack.Count > 0)
        {
            var dir = stack.Pop();

            IEnumerable<string> subDirectories;
            try
            {
                subDirectories = Directory.EnumerateDirectories(dir);
            }
            catch
            {
                continue;
            }

            foreach (var subDirectory in subDirectories)
            {
                directories.Add(subDirectory);
                stack.Push(subDirectory);
            }
        }

        return directories;
    }

    private void CleanUpDJson(List<string> assetFile)
    {
        assetFile.RemoveAll(f => f.EndsWith(DJsonExtension, StringComparison.OrdinalIgnoreCase));
    }

    private void DJsonCleanUp(IReadOnlyList<string> dJsonFiles)
    {
        for (var i = 0; i < dJsonFiles.Count; i++)
        {
            var fileAddress = dJsonFiles[i].Substring(0, dJsonFiles[i].Length - DJsonExtension.Length);
            var assetIdentifier = JsonConvert.DeserializeObject<AssetIdentifier>(File.ReadAllText(dJsonFiles[i]));

            if (assetIdentifier == null)
            {
                File.Delete(dJsonFiles[i]);
                continue;
            }

            if (assetIdentifier.IsDirectory)
            {
                if (!Directory.Exists(fileAddress))
                {
                    Directory.Delete(dJsonFiles[i]);
                    continue;
                }
            }
            else
            {
                if (!File.Exists(fileAddress))
                {
                    File.Delete(dJsonFiles[i]);
                    continue;
                }
            }
            
            _assets.Add(fileAddress, assetIdentifier);
            
        }
    }

    private void DJsonCreate(IReadOnlyList<string> assetFile, List<string> dJsonFiles)
    {
        for (var i = 0; i < assetFile.Count; i++)
        {
            var pathDJson = $"{assetFile[i]}{DJsonExtension}";
            
            if (File.Exists(pathDJson))
                continue;

            var identifier = new AssetIdentifier()
            {
                IsDirectory = Directory.Exists(assetFile[i])
            };
            identifier.SetSha(assetFile[i].Replace(_folderPath, ""));
            
            File.WriteAllText(pathDJson, JsonConvert.SerializeObject(identifier));
            dJsonFiles.Add(pathDJson);
        }
    }

    private void AssetsLinks(IReadOnlyList<string> assetFiles, IReadOnlyList<string> dJsonFiles)
    {
        for (var i = 0; i < assetFiles.Count; i++)
        {
            var path = assetFiles[i];
            var jsonPath = dJsonFiles[i];

            var assetIdentifier = JsonConvert.DeserializeObject<AssetIdentifier>(File.ReadAllText(jsonPath));
            if (assetIdentifier == null)
            {
                continue;
            }

            _assets.TryAdd(path, assetIdentifier);
        }
    }


    public void Save()
    {
        throw new NotImplementedException();
    }
}