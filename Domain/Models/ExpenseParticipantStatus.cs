using Domain.Enums;

namespace Domain.Models;

public class ExpenseParticipantStatus
{
    public ExpenseParticipantStatusId Id { get; init; }
    public string Name { get; init; } = default!;
}