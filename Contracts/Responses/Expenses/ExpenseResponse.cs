using Contracts.Responses.ExpenseCategories;
using Contracts.Responses.ExpenseItems;
using Contracts.Responses.ExpenseParticipants;
using Contracts.Responses.ExpenseTypes;

namespace Contracts.Responses.Expenses;

public record ExpenseResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required Guid CreatorId { get; init; }
    public required ExpenseTypeResponse ExpenseType { get; init; }
    public required ExpenseCategoryResponse Category { get; init; }
    public required string DateTime { get; init; }
    public ICollection<ExpenseParticipantResponse> Participants { get; init; } = new List<ExpenseParticipantResponse>();
    public ICollection<ExpenseItemResponse> ExpenseItems { get; init; } = new List<ExpenseItemResponse>();
    public ICollection<ExpenseMultiplierResponse> Multipliers { get; init; } = new List<ExpenseMultiplierResponse>();
}