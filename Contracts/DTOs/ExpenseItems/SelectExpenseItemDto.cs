namespace Contracts.DTOs.ExpenseItems;

public record SelectExpenseItemDto
{
    public required Guid ExpenseId { get; init; }
    public required Guid ExpenseItemId { get; init; }
    public required Guid CustomerId { get; init; }
}
