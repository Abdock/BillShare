using Domain.Base;

namespace Domain.Models;

public class Password : BaseEntity
{
    public Guid CustomerId { get; init; }
    public Customer Customer { get; init; } = default!;
    public string Salt { get; init; } = default!;
    public string EncryptedPassword { get; init; } = default!;
}