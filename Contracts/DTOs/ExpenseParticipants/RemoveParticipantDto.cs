namespace Contracts.DTOs.ExpenseParticipants;

public record RemoveParticipantDto
{
    public required Guid UserId { get; init; }
}