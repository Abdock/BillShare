namespace Contracts.DTOs.Reports;

public record Report
{
    public required decimal TotalSpendings { get; init; }
    public required int ExpenseCount { get; init; }
    public required IReadOnlyCollection<CategorySpend> CategoriesSpendings { get; init; }
}