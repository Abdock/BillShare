namespace BillShare.Requests.ExpenseCategories;

public record CreateExpenseCategoryRequest
{
    public required string CategoryName { get; init; }
    public required Guid IconId { get; init; }
}