namespace Contracts.Responses.ExpenseParticipants;

public record ExpenseParticipantActionResponse
{
    public required string RemoveParticipantUrl { get; init; }
}