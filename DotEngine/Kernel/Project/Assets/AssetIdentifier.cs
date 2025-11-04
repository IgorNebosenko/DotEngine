using System.Security.Cryptography;
using System.Text;

namespace Kernel.Project.Assets;

[Serializable]
public class AssetIdentifier
{
    private static SHA256 sha256 = SHA256.Create();
    
    public string Sha256Hash;
    public bool IsDirectory;

    public void SetSha(string shortPath)
    {
        var hashBytes = sha256.ComputeHash(Encoding.Unicode.GetBytes(shortPath));
        var builder = new StringBuilder();

        for (var i = 0; i < hashBytes.Length; i++)
            builder.Append(hashBytes[i].ToString("x2"));

        Sha256Hash = builder.ToString();
    }

}