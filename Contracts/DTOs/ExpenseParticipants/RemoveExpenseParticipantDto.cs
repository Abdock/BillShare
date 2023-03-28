namespace Contracts.DTOs.ExpenseParticipants;

public record RemoveExpenseParticipantDto
{
    public required Guid ExpenseId { get; init; }
    public required Guid ParticipantId { get; init; }
}