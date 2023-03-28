namespace Contracts.DTOs.ExpenseItems;

public record AddExpenseItemDto
{
    public required Guid ExpenseId { get; init; }
    public required Guid CustomerId { get; init; }
    public required string Name { get; init; }
    public required int Count { get; init; }
    public required decimal Amount { get; init; }
}