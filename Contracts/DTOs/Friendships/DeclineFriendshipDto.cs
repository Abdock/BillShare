namespace Contracts.DTOs.Friendships;

public record DeclineFriendshipDto
{
    public required Guid UserId { get; init; }
    public required Guid FriendId { get; init; }
}