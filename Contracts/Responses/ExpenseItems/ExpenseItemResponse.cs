namespace Contracts.Responses.ExpenseItems;

public record ExpenseItemResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required int Count { get; init; }
    public required int Amount { get; init; }
    public required ICollection<Guid> SelectedByParticipants { get; init; } = new List<Guid>();
    public required ExpenseItemActionResponse Actions { get; init; }
}