using Contracts.DTOs.General;
using Services.Abstractions;

namespace Services;

public class DriveStorageService : IStorageService
{
    private const string GlobalPath = "./BillShare/Images/";
    
    public async Task<string> WriteDataAsync(StorageFile file, CancellationToken cancellationToken = default)
    {
        var data = Convert.FromBase64String(file.Data);
        var filePath = GetFilePath(file);
        await File.WriteAllBytesAsync(filePath, data, cancellationToken);
        return filePath;
    }

    private static string GetFilePath(StorageFile file)
    {
        var info = new DirectoryInfo(GlobalPath);
        if (!info.Exists)
        {
            info.Create();
        }

        return $"{info.FullName}/{file.Id}.{file.Extension}";
    }
}