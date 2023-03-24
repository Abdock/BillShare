namespace Contracts.Responses.Icons;

public record IconResponse
{
    public required Guid IconId { get; init; }
    public required string IconUrl { get; init; }
}