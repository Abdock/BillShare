namespace Contracts.DTOs.Friendships;

public record AcceptFriendshipDto
{
    public required Guid UserId { get; init; }
    public required Guid FriendshipId { get; init; }
}