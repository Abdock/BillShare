namespace Contracts.DTOs.Friendships;

public record CreateFriendshipRequestDto
{
    public required Guid UserId { get; init; }
}