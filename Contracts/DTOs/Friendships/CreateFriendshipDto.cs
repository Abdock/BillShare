namespace Contracts.DTOs.Friendships;

public record CreateFriendshipDto
{
    public required Guid ReceiverId { get; init; }
    public required Guid SenderId { get; init; }
}