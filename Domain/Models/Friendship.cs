using Domain.Base;
using Domain.Enums;

namespace Domain.Models;

public class Friendship : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid FriendId { get; set; }
    public Customer User { get; set; } = default!;
    public Customer Friend { get; set; } = default!;
    public FriendshipStatusId StatusId { get; set; }
    public FriendshipStatus Status { get; set; } = default!;
}