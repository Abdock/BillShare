using Domain.Base;

namespace Domain.Models;

public class Icon : BaseEntity
{
    public string Url { get; init; } = default!;
    public Guid? ExpenseCategoryId { get; set; }
    public CustomExpenseCategory? ExpenseCategory { get; set; }
}