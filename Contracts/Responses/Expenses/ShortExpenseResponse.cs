using Contracts.Responses.ExpenseCategories;
using Contracts.Responses.ExpenseTypes;

namespace Contracts.Responses.Expenses;

public record ShortExpenseResponse
{
    public required Guid ExpenseId { get; init; }
    public required decimal Amount { get; init; }
    public required ExpenseTypeResponse ExpenseType { get; init; }
    public required ExpenseCategoryResponse Category { get; init; }
    public required string DateTime { get; init; }
}