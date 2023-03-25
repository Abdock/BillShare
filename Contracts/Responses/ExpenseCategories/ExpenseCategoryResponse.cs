namespace Contracts.Responses.ExpenseCategories;

public record ExpenseCategoryResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string IconUrl { get; init; }
}