namespace BillShare.Requests.ExpenseParticipants;

public record AddExpenseParticipantRequest
{
    public required Guid UserId { get; init; }
}