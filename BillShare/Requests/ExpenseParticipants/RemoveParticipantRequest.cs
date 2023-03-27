namespace BillShare.Requests.ExpenseParticipants;

public class RemoveParticipantRequest
{
    public required Guid UserId { get; init; }
}