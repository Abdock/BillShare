using Domain.Enums;

namespace Domain.Models;

public class FriendshipStatus
{
    public FriendshipStatusId Id { get; init; }
    public string Name { get; init; } = default!;
}