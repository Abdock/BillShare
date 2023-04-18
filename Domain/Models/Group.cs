using Domain.Base;

namespace Domain.Models;

public class Group : BaseEntity
{
    public string GroupName { get; init; } = default!;
    public Guid CreatorId { get; init; }
    public Customer Creator { get; init; } = default!;
    public ICollection<Customer> Participants { get; init; } = new HashSet<Customer>();
}