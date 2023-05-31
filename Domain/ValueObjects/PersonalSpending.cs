namespace Domain.ValueObjects;

public record PersonalSpending
{
    public required Guid CustomerId { get; init; }
    public required Spending Spends { get; init; }
}