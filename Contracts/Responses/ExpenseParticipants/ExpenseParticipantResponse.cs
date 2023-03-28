namespace Contracts.Responses.ExpenseParticipants;

public record ExpenseParticipantResponse
{
    public required Guid ParticipantId { get; init; }
    public required Guid UserId { get; init; }
    public required string Name { get; init; }
    public required string AvatarUrl { get; init; }
    public required ExpenseParticipantActionResponse Actions { get; set; }
}