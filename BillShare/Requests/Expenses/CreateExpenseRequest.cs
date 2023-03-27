using BillShare.Requests.ExpenseItems;
using BillShare.Requests.ExpenseParticipants;
using Domain.Enums;

namespace BillShare.Requests.Expenses;

public record CreateExpenseRequest
{
    public required ExpenseTypeId ExpenseTypeId { get; init; }
    public required Guid CategoryId { get; init; }
    public required Guid AccountId { get; init; }
    public required int Amount { get; init; }
    public ICollection<AddParticipantRequest> Participants { get; init; } = new List<AddParticipantRequest>();
    public ICollection<ExpenseItemRequest> Items { get; init; } = new List<ExpenseItemRequest>();
}