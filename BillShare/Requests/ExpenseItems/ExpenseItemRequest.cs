namespace BillShare.Requests.ExpenseItems;

public record ExpenseItemRequest
{
    public required string Name { get; init; }
    public required int Count { get; init; }
    public required int Amount { get; init; }
}