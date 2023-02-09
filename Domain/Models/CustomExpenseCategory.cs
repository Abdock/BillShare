using Domain.Base;

namespace Domain.Models;

public class CustomExpenseCategory : BaseEntity
{
    public Guid CustomerId { get; init; }
    public Customer Customer { get; init; } = default!;
    public string Name { get; set; } = default!;
    public Guid IconId { get; set; }
    public Icon Icon { get; set; } = default!;
}