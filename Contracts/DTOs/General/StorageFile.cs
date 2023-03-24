namespace Contracts.DTOs.General;

public record StorageFile
{
    public required Guid Id { get; init; }
    /// <summary>
    /// Base64 string data
    /// </summary>
    public required string Data { get; init; }
    public required string Extension { get; init; }
}