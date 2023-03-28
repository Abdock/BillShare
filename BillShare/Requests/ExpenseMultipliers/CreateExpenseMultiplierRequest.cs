namespace BillShare.Requests.ExpenseMultipliers;

public record CreateExpenseMultiplierRequest
{
    public required string Name { get; init; }
    public required int CostMultiplierPercent { get; init; }
}