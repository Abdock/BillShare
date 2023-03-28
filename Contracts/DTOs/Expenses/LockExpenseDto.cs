namespace Contracts.DTOs.Expenses;

public record LockExpenseDto
{
    public required Guid ExpenseId { get; init; }
    public required Guid UserId { get; init; }
}