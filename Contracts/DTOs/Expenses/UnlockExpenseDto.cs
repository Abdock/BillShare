namespace Contracts.DTOs.Expenses;

public record UnlockExpenseDto
{
    public required Guid ExpenseId { get; init; }
    public required Guid UserId { get; init; }
}