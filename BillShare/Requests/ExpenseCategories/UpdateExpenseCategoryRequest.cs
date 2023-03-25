namespace BillShare.Requests.ExpenseCategories;

public record UpdateExpenseCategoryRequest
{
    public required string Name { get; init; }
}