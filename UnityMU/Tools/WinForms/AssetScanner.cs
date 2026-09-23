using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UnityMU.AssetTool;

public sealed class AssetScanner
{
    private static readonly HashSet<string> ModelExtensions = new(StringComparer.OrdinalIgnoreCase) { ".bmd", ".smd" };
    private static readonly HashSet<string> TextureExtensions = new(StringComparer.OrdinalIgnoreCase) { ".ozj", ".ozt", ".tga", ".jpg", ".jpeg", ".png", ".bmp" };

    public IReadOnlyList<AssetRecord> Scan(string root, IProgress<int>? progress = null)
    {
        var files = Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories).ToArray();
        var result = new List<AssetRecord>(files.Length);
        for (var i = 0; i < files.Length; i++)
        {
            var full = Path.GetFullPath(files[i]);
            var relative = Path.GetRelativePath(root, full).Replace(Path.DirectorySeparatorChar, '/');
            var extension = Path.GetExtension(full).ToLowerInvariant();
            var category = ModelExtensions.Contains(extension) ? "Model" : TextureExtensions.Contains(extension) ? "Texture" : "Other";
            var isPlayer = relative.Split('/').Any(x => x.Equals("Player", StringComparison.OrdinalIgnoreCase));
            var record = new AssetRecord
            {
                RelativePath = relative,
                Name = Path.GetFileName(full),
                Extension = extension,
                Category = category,
                IsPlayerResource = isPlayer,
                Size = new FileInfo(full).Length,
                Sha256 = ComputeSha256(full),
                Diagnostics = Diagnose(full, extension)
            };
            result.Add(record);
            progress?.Report((i + 1) * 100 / Math.Max(1, files.Length));
        }
        return result;
    }

    public static void WriteJson(string output, string root, IReadOnlyList<AssetRecord> assets)
    {
        var document = new AssetManifest { Format = "MuAssetManifest", Version = 1, Root = root, Files = assets };
        var options = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        File.WriteAllText(output, JsonSerializer.Serialize(document, options));
    }

    public static void WriteCsv(string output, IReadOnlyList<AssetRecord> assets)
    {
        using var writer = new StreamWriter(output, false, System.Text.Encoding.UTF8);
        writer.WriteLine("RelativePath,Name,Extension,Category,IsPlayerResource,Size,Sha256,Diagnostics");
        foreach (var asset in assets)
        {
            var diagnostics = string.Join(" | ", asset.Diagnostics);
            writer.WriteLine(string.Join(',', Quote(asset.RelativePath), Quote(asset.Name), Quote(asset.Extension), Quote(asset.Category), asset.IsPlayerResource, asset.Size, Quote(asset.Sha256), Quote(diagnostics)));
        }
    }

    private static string ComputeSha256(string path)
    {
        using var stream = File.OpenRead(path);
        return Convert.ToHexString(SHA256.HashData(stream)).ToLowerInvariant();
    }

    private static List<string> Diagnose(string path, string extension)
    {
        var result = new List<string>();
        if (extension == ".bmd")
        {
            var length = new FileInfo(path).Length;
            result.Add($"BMD binary candidate, {length} bytes");
            result.Add("Parser is intentionally disabled until the exact BMD version/layout is verified");
        }
        else if (extension is ".ozj" or ".ozt")
        {
            result.Add("MU texture candidate; OZJ/OZT decoder is not enabled yet");
        }
        return result;
    }

    private static string Quote(string value) => $"\"{value.Replace("\"", "\"\"")}\"";
}

public sealed class AssetManifest
{
    public string Format { get; set; } = "MuAssetManifest";
    public int Version { get; set; }
    public string Root { get; set; } = "";
    public IReadOnlyList<AssetRecord> Files { get; set; } = Array.Empty<AssetRecord>();
}

public sealed class AssetRecord
{
    public string RelativePath { get; set; } = "";
    public string Name { get; set; } = "";
    public string Extension { get; set; } = "";
    public string Category { get; set; } = "";
    public bool IsPlayerResource { get; set; }
    public long Size { get; set; }
    public string Sha256 { get; set; } = "";
    public List<string> Diagnostics { get; set; } = new();
}
