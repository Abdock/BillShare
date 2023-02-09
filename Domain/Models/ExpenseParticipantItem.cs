using Domain.Base;
using Domain.Enums;

namespace Domain.Models;

public class ExpenseParticipantItem : BaseEntity
{
    public Guid ExpenseParticipantId { get; init; }
    public ExpenseParticipant Participant { get; init; } = default!;
    public Guid ItemId { get; set; }
    public ExpenseItem Item { get; set; } = default!;
    public ExpenseParticipantItemStatusId StatusId { get; set; }
    public ExpenseParticipantItemStatus Status { get; set; } = default!;
}