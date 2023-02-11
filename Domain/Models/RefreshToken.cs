using Domain.Base;

namespace Domain.Models;

public class RefreshToken : BaseEntity
{
    public string Token { get; init; } = default!;
    public Guid OwnerId { get; init; }
    public Customer Owner { get; init; } = default!;
    public DateTime ExpirationDateTime { get; init; }
}