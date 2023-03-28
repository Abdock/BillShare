namespace Contracts.DTOs.ExpenseParticipants;

public record AddExpenseParticipantDto
{
    public required Guid ExpenseId { get; init; }
    public required Guid UserId { get; init; }
}