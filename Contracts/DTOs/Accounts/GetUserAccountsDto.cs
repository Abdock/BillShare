namespace Contracts.DTOs.Accounts;

public record GetUserAccountsDto
{
    public required Guid UserId { get; init; }
}