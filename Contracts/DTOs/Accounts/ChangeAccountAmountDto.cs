namespace Contracts.DTOs.Accounts;

public record ChangeAccountAmountDto
{
    public required Guid UserId { get; init; }
    public required Guid AccountId { get; init; }
    public required decimal Amount { get; init; }
}