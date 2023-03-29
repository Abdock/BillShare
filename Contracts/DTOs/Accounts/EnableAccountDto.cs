namespace Contracts.DTOs.Accounts;

public record EnableAccountDto
{
    public required Guid UserId { get; init; }
    public required Guid AccountId { get; init; }
}