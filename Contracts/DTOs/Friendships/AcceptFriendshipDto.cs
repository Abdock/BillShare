namespace Contracts.DTOs.Friendships;

public record AcceptFriendshipDto
{
    public required Guid FriendshipId { get; init; }
}