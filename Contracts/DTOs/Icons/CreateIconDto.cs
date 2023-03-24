namespace Contracts.DTOs.Icons;

public record CreateIconDto
{
    public required string IconImageData { get; init; }
    public required string Extension { get; init; }
}