namespace BillShare.Requests.ExpenseParticipants;

public record AddParticipantRequest
{
    public required Guid UserId { get; init; }
}