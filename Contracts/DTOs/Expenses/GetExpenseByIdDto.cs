namespace Contracts.DTOs.Expenses;

public record GetExpenseByIdDto
{
    public required Guid ExpenseId { get; init; }
    public required Guid CustomerId { get; init; }
    public required Uri RemoveParticipantUrl { get; init; }
}