using Domain.Base;

namespace Domain.Models;

public class Account : BaseEntity
{
    public Guid UserId { get; init; }
    public Customer Customer { get; init; } = default!;
    public string ExternalId { get; init; } = default!;
    public decimal Amount { get; set; }
    public string Name { get; init; } = default!;
    public ICollection<Expense> Expenses { get; init; } = default!;
}