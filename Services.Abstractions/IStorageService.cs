using Contracts.DTOs.General;

namespace Services.Abstractions;

public interface IStorageService
{
    Task<string> WriteDataAsync(StorageFile file, CancellationToken cancellationToken = default);
}