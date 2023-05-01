using Domain.Base;
using Domain.Enums;

namespace Domain.Models;

public class ExpenseItem : BaseEntity
{
    public Guid ExpenseId { get; init; }
    public Expense Expense { get; init; } = default!;
    public string Name { get; init; } = default!;
    public int Count { get; init; }
    public decimal Amount { get; init; }
    public ExpenseItemStatusId StatusId { get; init; }
    public ExpenseItemStatus Status { get; init; } = default!;

    public virtual ICollection<ExpenseParticipantItem> ExpenseParticipantItems { get; init; } =
        new HashSet<ExpenseParticipantItem>();
}