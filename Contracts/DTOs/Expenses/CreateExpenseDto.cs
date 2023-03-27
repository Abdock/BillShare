using Contracts.DTOs.ExpenseItems;
using Contracts.DTOs.ExpenseParticipants;
using Domain.Enums;

namespace Contracts.DTOs.Expenses;

public record CreateExpenseDto
{
    public required ExpenseTypeId ExpenseTypeId { get; init; }
    public required Guid CategoryId { get; init; }
    public required Guid AccountId { get; init; }
    public required int Amount { get; init; }
    public ICollection<AddParticipantDto> Participants { get; init; } = new List<AddParticipantDto>();
    public ICollection<CreateExpenseItemDto> Items { get; init; } = new List<CreateExpenseItemDto>();
}