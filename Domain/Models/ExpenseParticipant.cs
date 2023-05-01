using Domain.Base;
using Domain.Enums;

namespace Domain.Models;

public class ExpenseParticipant : BaseEntity
{
    public Guid CustomerId { get; init; }
    public Customer Customer { get; init; } = default!;
    public Guid ExpenseId { get; init; }
    public Expense Expense { get; init; } = default!;
    public ExpenseParticipantStatusId StatusId { get; init; }
    public ExpenseParticipantStatus Status { get; init; } = default!;

    public virtual ICollection<ExpenseParticipantItem> ExpenseParticipantItems { get; init; } =
        new HashSet<ExpenseParticipantItem>();
}