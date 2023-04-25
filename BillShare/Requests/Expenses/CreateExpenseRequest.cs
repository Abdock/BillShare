using BillShare.Requests.ExpenseItems;
using BillShare.Requests.ExpenseMultipliers;
using BillShare.Requests.ExpenseParticipants;
using Domain.Enums;

namespace BillShare.Requests.Expenses;

public record CreateExpenseRequest
{
    public required string Name { get; init; }
    public required ExpenseTypeId ExpenseTypeId { get; init; }
    public required Guid CategoryId { get; init; }
    public required Guid AccountId { get; init; }
    public required int Amount { get; init; }
    public ICollection<AddExpenseParticipantRequest> Participants { get; init; } = new List<AddExpenseParticipantRequest>();
    public ICollection<AddExpenseItemRequest> Items { get; init; } = new List<AddExpenseItemRequest>();

    public ICollection<CreateExpenseMultiplierRequest> Multipliers { get; init; } =
        new List<CreateExpenseMultiplierRequest>();
}