namespace Contracts.DTOs.ExpenseParticipants;

public record AddParticipantDto
{
    public required Guid UserId { get; init; }
}