namespace Contracts.DTOs.ExpenseMultipliers;

public record CreateExpenseMultiplierDto
{
    public required string Name { get; init; }
    public required decimal Multiplier { get; init; }
}