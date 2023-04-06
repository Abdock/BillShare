using Contracts.DTOs.General;
using Services.Abstractions;

namespace Services;

public class DriveStorageService : IStorageService
{
    private const string GlobalPath = "Images/";
    private readonly string _path;

    public DriveStorageService(string folderPath)
    {
        _path = $"{folderPath}/{GlobalPath}";
    }

    public async Task<string> WriteDataAsync(StorageFile file, CancellationToken cancellationToken = default)
    {
        var data = Convert.FromBase64String(file.Data);
        var filePath = GetFilePath(file);
        await File.WriteAllBytesAsync(filePath, data, cancellationToken);
        return filePath;
    }

    private string GetFilePath(StorageFile file)
    {
        var info = new DirectoryInfo(_path);
        if (!info.Exists)
        {
            info.Create();
        }

        return $"{info.FullName}/{file.Id}.{file.Extension}";
    }
}