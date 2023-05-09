namespace Domain.ValueObjects;

public record Spending
{
    public required Guid CategoryId { get; init; }
    public required string CategoryName { get; init; }
    public required decimal TotalSpend { get; init; }
    public required int ExpensesCount { get; init; }
}