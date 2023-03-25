namespace Contracts.DTOs.ExpenseCategories;

public record UpdateExpenseCategoryDto
{
    public required Guid CategoryId { get; init; }
    public required Guid UserId { get; init; }
    public required string Name { get; init; }
}