namespace Contracts.DTOs.Accounts;

public record GetPaidExpensesDto
{
    public required Guid UserId { get; init; }
    public required Guid AccountId { get; init; }
}