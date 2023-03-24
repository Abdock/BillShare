namespace Contracts.DTOs.Friendships;

public record DeclineFriendshipDto
{
    public required Guid FriendshipId { get; init; }
}