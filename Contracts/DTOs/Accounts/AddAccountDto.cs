namespace Contracts.DTOs.Accounts;

public record AddAccountDto
{
    public required Guid UserId { get; init; }
    public required decimal Amount { get; init; }
    public required string Name { get; init; }
    public string? ExternalId { get; init; }
}