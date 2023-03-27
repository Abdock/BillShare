namespace Contracts.DTOs.ExpenseItems;

public record CreateExpenseItemDto
{
    public required string Name { get; init; }
    public required int Count { get; init; }
    public required int Amount { get; init; }
}