using Domain.Base;
using Domain.Enums;

namespace Domain.Models;

public class Account : BaseEntity
{
    public Guid UserId { get; init; }
    public Customer Customer { get; init; } = default!;
    public string? ExternalId { get; set; }
    public decimal Amount { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<Expense> Expenses { get; init; } = default!;
    public AccountStatusId StatusId { get; set; }
    public AccountStatus Status { get; init; } = default!;
}