namespace Contracts.Responses.Expenses;

public record ExpenseMultiplierResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required int CostMultiplierPercent { get; init; }
}