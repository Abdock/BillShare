namespace Contracts.DTOs.ExpenseParticipants;

public record AddNewExpenseParticipantDto
{
    public required Guid UserId { get; init; }
}