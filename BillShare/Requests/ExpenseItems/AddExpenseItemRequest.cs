namespace BillShare.Requests.ExpenseItems;

public record AddExpenseItemRequest
{
    public required string Name { get; init; }
    public required int Count { get; init; }
    public required decimal Amount { get; init; }
}