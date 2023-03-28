using Contracts.DTOs.ExpenseItems;
using Contracts.DTOs.ExpenseMultipliers;
using Contracts.DTOs.ExpenseParticipants;
using Domain.Enums;

namespace Contracts.DTOs.Expenses;

public record CreateExpenseDto
{
    public required Guid CreatorId { get; init; }
    public required ExpenseTypeId ExpenseTypeId { get; init; }
    public required Guid CategoryId { get; init; }
    public required Guid AccountId { get; init; }
    public required int Amount { get; init; }
    public ICollection<AddExpenseParticipantDto> ExpenseParticipants { get; init; } = new List<AddExpenseParticipantDto>();
    public ICollection<CreateExpenseItemDto> ExpenseItems { get; init; } = new List<CreateExpenseItemDto>();
    public ICollection<CreateExpenseMultiplierDto> ExpenseMultipliers = new List<CreateExpenseMultiplierDto>();
    public required Uri RemoveParticipantUrl { get; init; }
}