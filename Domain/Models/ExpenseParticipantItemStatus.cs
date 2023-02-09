using Domain.Enums;

namespace Domain.Models;

public class ExpenseParticipantItemStatus
{
    public ExpenseParticipantItemStatusId Id { get; init; }
    public string Name { get; init; } = default!;
}