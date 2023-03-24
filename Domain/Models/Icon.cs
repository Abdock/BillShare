using Domain.Base;

namespace Domain.Models;

public class Icon : BaseEntity
{
    public string Url { get; init; } = default!;
    public Guid? ExpenseCategoryId { get; init; }
    public CustomExpenseCategory? ExpenseCategory { get; init; }
}