namespace Contracts.DTOs.Accounts;

public record GetUserAccountDto
{
    public required Guid UserId { get; init; }
    public required Guid AccountId { get; init; }
}