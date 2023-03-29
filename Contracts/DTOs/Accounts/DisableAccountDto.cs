namespace Contracts.DTOs.Accounts;

public record DisableAccountDto
{
    public required Guid UserId { get; init; }
    public required Guid AccountId { get; init; }
}