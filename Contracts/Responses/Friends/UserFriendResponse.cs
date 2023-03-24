namespace Contracts.Responses.Friends;

public record UserFriendResponse
{
    public required Guid UserId { get; init; }
    public required string UserName { get; init; }
    public required string UserAvatarUrl { get; init; }
}