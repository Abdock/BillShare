using Domain.Base;

namespace Domain.Models;

public class ExpenseMultiplier : BaseEntity
{
    public Guid ExpenseId { get; set; }
    public Expense Expense { get; set; } = default!;
    public decimal Multiplier { get; set; }
    public string Name { get; set; } = default!;
}